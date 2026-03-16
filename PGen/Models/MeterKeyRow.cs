using System.Numerics;
using System.Text.RegularExpressions;

namespace PGen.Models;

internal sealed class MeterKeyRow
{
    public required string Msn { get; init; }
    public required string MeterType { get; init; }
    public required string Model { get; init; }
    public int SetIndex { get; init; }
    public string? PoNumber { get; init; }

    private string _ak8 = string.Empty;
    private string _ek8 = string.Empty;
    private string _ak32 = string.Empty;
    private string _ek32 = string.Empty;

    public string Ak8
    {
        get => Normalize(_ak8, 8);
        init { _ak8 = value ?? string.Empty; }
    }

    public string Ek8
    {
        get => Normalize(_ek8, 8);
        init { _ek8 = value ?? string.Empty; }
    }

    public string Ak32
    {
        get => Normalize(_ak32, 32);
        init { _ak32 = value ?? string.Empty; }
    }

    public string Ek32
    {
        get => Normalize(_ek32, 32);
        init { _ek32 = value ?? string.Empty; }
    }

    private static string Normalize(string raw, int expectedLength)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return raw ?? string.Empty;

        // if already digits, keep value; otherwise try parse hex
        string normalized;
        if (Regex.IsMatch(raw, "^[0-9]+$"))
        {
            normalized = raw;
        }
        else
        {
            try
            {
                var value = BigInteger.Parse(raw, System.Globalization.NumberStyles.HexNumber);
                if (value.Sign < 0) value = BigInteger.Negate(value);
                normalized = value.ToString();
            }
            catch
            {
                normalized = raw;
            }
        }

        if (expectedLength > 0)
        {
            if (normalized.Length > expectedLength)
                normalized = normalized[^expectedLength..];
            else if (normalized.Length < expectedLength)
                normalized = normalized.PadLeft(expectedLength, '0');
        }

        return normalized;
    }
}

