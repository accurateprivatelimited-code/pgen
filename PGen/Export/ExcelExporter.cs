using ClosedXML.Excel;
using PGen.Models;

namespace PGen.Export;

internal static class ExcelExporter
{
    public static void Export8Digit(string path, IReadOnlyList<MeterKeyRow> rows)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Keys8");

        // Group rows by (Msn, MeterType, Model) like the GridView
        var grouped = rows
            .GroupBy(r => (Msn: r.Msn, MeterType: r.MeterType, Model: r.Model ?? string.Empty))
            .ToList();

        // Find max set index for column generation
        var maxSet = 0;
        if (grouped.Count > 0)
        {
            maxSet = grouped.SelectMany(g => g).Select(s => s.SetIndex).DefaultIfEmpty(0).Max();
        }

        // Create headers matching GridView format
        var col = 1;
        ws.Cell(1, col++).Value = "MSN";
        ws.Cell(1, col++).Value = "Type";
        for (var i = 1; i <= maxSet; i++)
        {
            ws.Cell(1, col++).Value = $"AK8({i})";
            ws.Cell(1, col++).Value = $"EK8({i})";
        }

        // Apply text format to header row
        ws.Row(1).Style.NumberFormat.SetFormat("@");

        var row = 2;
        foreach (var group in grouped)
        {
            col = 1;
            ws.Cell(row, col++).Value = group.Key.Msn;
            ws.Cell(row, col++).Value = group.Key.MeterType;

            var setByIndex = group.ToDictionary(r => r.SetIndex);
            for (var i = 1; i <= maxSet; i++)
            {
                if (setByIndex.TryGetValue(i, out var r))
                {
                    ws.Cell(row, col++).Value = r.Ak8;
                    ws.Cell(row, col++).Value = r.Ek8;
                }
                else
                {
                    ws.Cell(row, col++).Value = "";
                    ws.Cell(row, col++).Value = "";
                }
            }

            // Apply text format to the entire row
            ws.Row(row).Style.NumberFormat.SetFormat("@");
            row++;
        }

        ws.RangeUsed()?.SetAutoFilter();
        ws.Columns().AdjustToContents();
        wb.SaveAs(path);
    }

    public static void Export32Digit(string path, IReadOnlyList<MeterKeyRow> rows)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Keys32");

        // Group rows by (Msn, MeterType, Model) like the GridView
        var grouped = rows
            .GroupBy(r => (Msn: r.Msn, MeterType: r.MeterType, Model: r.Model ?? string.Empty))
            .ToList();

        // Find max set index for column generation
        var maxSet = 0;
        if (grouped.Count > 0)
        {
            maxSet = grouped.SelectMany(g => g).Select(s => s.SetIndex).DefaultIfEmpty(0).Max();
        }

        // Create headers matching GridView format
        var col = 1;
        ws.Cell(1, col++).Value = "MSN";
        ws.Cell(1, col++).Value = "Type";
        for (var i = 1; i <= maxSet; i++)
        {
            ws.Cell(1, col++).Value = $"AK32({i})";
            ws.Cell(1, col++).Value = $"EK32({i})";
        }

        // Apply text format to header row
        ws.Row(1).Style.NumberFormat.SetFormat("@");

        var row = 2;
        foreach (var group in grouped)
        {
            col = 1;
            ws.Cell(row, col++).Value = group.Key.Msn;
            ws.Cell(row, col++).Value = group.Key.MeterType;

            var setByIndex = group.ToDictionary(r => r.SetIndex);
            for (var i = 1; i <= maxSet; i++)
            {
                if (setByIndex.TryGetValue(i, out var r))
                {
                    ws.Cell(row, col++).Value = r.Ak32;
                    ws.Cell(row, col++).Value = r.Ek32;
                }
                else
                {
                    ws.Cell(row, col++).Value = "";
                    ws.Cell(row, col++).Value = "";
                }
            }

            // Apply text format to the entire row
            ws.Row(row).Style.NumberFormat.SetFormat("@");
            row++;
        }

        ws.RangeUsed()?.SetAutoFilter();
        ws.Columns().AdjustToContents();
        wb.SaveAs(path);
    }
}

