using System.Text.RegularExpressions;

namespace PGen.Services;

internal static class MsnRangeParser
{
    private static readonly Regex DigitsOnly = new(@"^\d+$", RegexOptions.Compiled);
    private static readonly Regex Range = new(@"^\s*(\d+)\s*-\s*(\d+)\s*$", RegexOptions.Compiled);

    public static IReadOnlyList<string> Parse(string input)
    {
        input = (input ?? string.Empty).Trim();
        if (input.Length == 0)
            throw new ArgumentException("MSN is required.");

        var m = Range.Match(input);
        if (m.Success)
        {
            var startRaw = m.Groups[1].Value;
            var endRaw = m.Groups[2].Value;

            EnsureLength(startRaw);
            EnsureLength(endRaw);

            var width = Math.Max(startRaw.Length, endRaw.Length);
            if (width is not (8 or 10 or 12))
                throw new ArgumentException("MSN length must be 8, 10, or 12 digits.");

            if (!long.TryParse(startRaw, out var start) || !long.TryParse(endRaw, out var end))
                throw new ArgumentException("Invalid range values.");
            if (end < start)
                throw new ArgumentException("Range end must be >= start.");

            var count = end - start + 1;
            if (count > 2_000_000)
                throw new ArgumentException("Range too large. Please use <= 2,000,000 items.");

            var list = new string[count];
            for (long i = 0; i < count; i++)
            {
                list[i] = (start + i).ToString().PadLeft(width, '0');
            }
            return list;
        }

        if (!DigitsOnly.IsMatch(input))
            throw new ArgumentException("MSN must be digits only, or a range like 1000-1050.");

        EnsureLength(input);
        return new[] { input };
    }

    private static void EnsureLength(string s)
    {
        if (s.Length is not (8 or 10 or 12))
            throw new ArgumentException("MSN length must be 8, 10, or 12 digits.");
    }
}

