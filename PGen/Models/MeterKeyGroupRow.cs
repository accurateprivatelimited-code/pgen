namespace PGen.Models;

/// <summary>
/// Represents one MSN with all its sets and keys grouped in a single row for display.
/// </summary>
internal sealed class MeterKeyGroupRow
{
    public required string Msn { get; init; }
    public required string MeterType { get; init; }
    public required string Model { get; init; }
    public required IReadOnlyList<MeterKeyRow> Sets { get; init; }

    /// <summary>
    /// Formatted string of all sets: "1: AK8/EK8 | 2: AK8/EK8 | ..."
    /// </summary>
    public string AllSetsDisplay => string.Join(" | ", Sets.OrderBy(s => s.SetIndex)
        .Select(s => $"{s.SetIndex}: {s.Ak8}/{s.Ek8}"));

    /// <summary>
    /// Formatted string of all sets with 32-digit keys for copy/export.
    /// </summary>
    public string AllSetsDisplay32 => string.Join(" | ", Sets.OrderBy(s => s.SetIndex)
        .Select(s => $"{s.SetIndex}: {s.Ak32}/{s.Ek32}"));
}
