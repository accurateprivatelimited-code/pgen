using System.Text.RegularExpressions;

namespace PGen.Services;

internal static class PoRangeParser
{
    public static IReadOnlyList<string> Parse(string input)
    {
        input = (input ?? string.Empty).Trim();
        if (input.Length == 0)
            return new string[0]; // Return empty array for empty input (PO Number is optional)

        // PO Number is a single identifier, not a range
        // Allow any format - no validation
        // Return single PO Number as array for consistency with existing code
        return new[] { input };
    }
}
