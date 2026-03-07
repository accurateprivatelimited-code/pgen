using System.Security.Cryptography;
using System.Text;
using System.Numerics;
using System.Globalization;
using PGen.Models;

namespace PGen.Services;

internal sealed class PasswordGeneratorService
{
    // NOTE: Replace this with your production algorithm/keys.
    // This implementation is deterministic and produces stable AK/EK values per MSN+Type+Model(+SetIndex).
    private static readonly byte[] MasterKey = SHA256.HashData(Encoding.UTF8.GetBytes("PGen::MasterKey::ReplaceMe"));

    public IReadOnlyList<MeterKeyRow> Generate(
        IReadOnlyList<string> msns,
        string meterType,
        string model,
        int setsPerMsn,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        if (setsPerMsn <= 0 || setsPerMsn > 1000)
            throw new ArgumentOutOfRangeException(nameof(setsPerMsn), "Sets must be between 1 and 1000.");

        var total = msns.Count * setsPerMsn;
        var rows = new MeterKeyRow[total];

        var useParallel = msns.Count >= 2000;
        var completed = 0;

        if (useParallel)
        {
            Parallel.For(
                fromInclusive: 0,
                toExclusive: msns.Count,
                new ParallelOptions { CancellationToken = cancellationToken },
                msnIndex =>
                {
                    var msn = msns[msnIndex];
                    for (var setIndex = 1; setIndex <= setsPerMsn; setIndex++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        var row = BuildRow(msn, meterType, model, setIndex);
                        rows[msnIndex * setsPerMsn + (setIndex - 1)] = row;
                    }

                    var done = Interlocked.Add(ref completed, setsPerMsn);
                    progress?.Report((int)(done * 100.0 / total));
                });
        }
        else
        {
            for (var msnIndex = 0; msnIndex < msns.Count; msnIndex++)
            {
                var msn = msns[msnIndex];
                for (var setIndex = 1; setIndex <= setsPerMsn; setIndex++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var row = BuildRow(msn, meterType, model, setIndex);
                    rows[msnIndex * setsPerMsn + (setIndex - 1)] = row;
                }

                completed += setsPerMsn;
                progress?.Report((int)(completed * 100.0 / total));
            }
        }

        progress?.Report(100);
        return rows;
    }

    private static MeterKeyRow BuildRow(string msn, string meterType, string model, int setIndex)
    {
        // derive raw hex values; only the first 4 bytes influence the 8‑digit keys
        var akHex = DeriveHex(msn, meterType, model, setIndex, purpose: "AK", bytes: 16);
        var ekHex = DeriveHex(msn, meterType, model, setIndex, purpose: "EK", bytes: 16);

        // eight‑digit keys come from first 8 hex characters (4 bytes)
        var ak8 = HexToDecimalFixed(akHex[..8], digits: 8);
        var ek8 = HexToDecimalFixed(ekHex[..8], digits: 8);

        // 32‑digit keys are simply the 8‑digit values padded with leading zeros
        var ak32 = ak8.PadLeft(32, '0');
        var ek32 = ek8.PadLeft(32, '0');

        return new MeterKeyRow
        {
            Msn = msn,
            MeterType = meterType,
            Model = model,
            SetIndex = setIndex,
            Ak8 = ak8,
            Ek8 = ek8,
            Ak32 = ak32,
            Ek32 = ek32
        };
    }

    private static string DeriveHex(string msn, string meterType, string model, int setIndex, string purpose, int bytes)
    {
        var msg = $"{msn}|{meterType}|{model}|{setIndex}|{purpose}";
        using var hmac = new HMACSHA256(MasterKey);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(msg));
        return Convert.ToHexString(hash.AsSpan(0, bytes));
    }

    /// <summary>
    /// Converts a hex string into a positive decimal representation.  The algorithm
    /// is deterministic and will always return a string containing only digits.
    /// </summary>
    private static string HexToDecimal(string hex)
    {
        // parse as a positive big integer (hex input may contain leading zeros)
        var value = System.Numerics.BigInteger.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        if (value.Sign < 0)
            value = System.Numerics.BigInteger.Negate(value);
        return value.ToString();
    }

    /// <summary>
    /// Like <see cref="HexToDecimal"/>, but ensures the resulting string has exactly
    /// <paramref name="digits"/> characters by truncating or padding with leading
    /// zeros.  Truncation keeps the rightmost digits (equivalent to modulo
    /// 10^digits).
    /// </summary>
    private static string HexToDecimalFixed(string hex, int digits)
    {
        var value = System.Numerics.BigInteger.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        if (value.Sign < 0)
            value = System.Numerics.BigInteger.Negate(value);
        if (digits <= 0)
            return value.ToString();
        var mod = BigInteger.Pow(10, digits);
        var truncated = value % mod;
        var result = truncated.ToString();
        if (result.Length < digits)
            result = result.PadLeft(digits, '0');
        return result;
    }
}

