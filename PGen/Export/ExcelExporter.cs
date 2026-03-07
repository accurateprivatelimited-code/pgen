using ClosedXML.Excel;
using PGen.Models;

namespace PGen.Export;

internal static class ExcelExporter
{
    public static void Export8Digit(string path, IReadOnlyList<MeterKeyRow> rows)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Keys8");

        ws.Cell(1, 1).Value = "MSN";
        ws.Cell(1, 1).Style.NumberFormat.SetFormat("@");
        ws.Cell(1, 2).Value = "AK8";
        ws.Cell(1, 2).Style.NumberFormat.SetFormat("@");
        ws.Cell(1, 3).Value = "EK8";
        ws.Cell(1, 3).Style.NumberFormat.SetFormat("@");

        for (var i = 0; i < rows.Count; i++)
        {
            var r = rows[i];
            var row = i + 2;
            ws.Cell(row, 1).Value = r.Msn;
            ws.Cell(row, 1).Style.NumberFormat.SetFormat("@");
            ws.Cell(row, 2).Value = r.Ak8;
            ws.Cell(row, 2).Style.NumberFormat.SetFormat("@");
            ws.Cell(row, 3).Value = r.Ek8;
            ws.Cell(row, 3).Style.NumberFormat.SetFormat("@");
        }

        ws.RangeUsed()?.SetAutoFilter();
        ws.Columns().AdjustToContents();
        wb.SaveAs(path);
    }

    public static void Export32Digit(string path, IReadOnlyList<MeterKeyRow> rows)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Keys32");

        ws.Cell(1, 1).Value = "MSN";
        ws.Cell(1, 1).Style.NumberFormat.SetFormat("@");
        ws.Cell(1, 2).Value = "AK32";
        ws.Cell(1, 2).Style.NumberFormat.SetFormat("@");
        ws.Cell(1, 3).Value = "EK32";
        ws.Cell(1, 3).Style.NumberFormat.SetFormat("@");

        for (var i = 0; i < rows.Count; i++)
        {
            var r = rows[i];
            var row = i + 2;
            ws.Cell(row, 1).Value = r.Msn;
            ws.Cell(row, 1).Style.NumberFormat.SetFormat("@");
            ws.Cell(row, 2).Value = r.Ak32;
            ws.Cell(row, 2).Style.NumberFormat.SetFormat("@");
            ws.Cell(row, 3).Value = r.Ek32;
            ws.Cell(row, 3).Style.NumberFormat.SetFormat("@");
        }

        ws.RangeUsed()?.SetAutoFilter();
        ws.Columns().AdjustToContents();
        wb.SaveAs(path);
    }
}

