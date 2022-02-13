using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using neophyte.api.Data.DTOs;

namespace neophyte.api.Data.Generators;

public static class ExcelGenerator
{
    public static byte[] ForEventAttendees(List<EventAttendeeDto> attendees)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Attendees");
        AddHeader(worksheet);
        AddBody(worksheet, attendees);
        return WorkbookToByteArray(workbook);
    }

    private static void AddHeader(IXLWorksheet worksheet)
    {
        worksheet.Range(1, 1, 1, 8).Style.Font.Bold = true;
        worksheet.Cell("A1").Value = "S/No";
        worksheet.Cell("B1").Value = "First Name";
        worksheet.Cell("C1").Value = "Last Name";
        worksheet.Cell("D1").Value = "Phone Number";
        worksheet.Cell("E1").Value = "Venue";
        worksheet.Cell("F1").Value = "Seat Type";
        worksheet.Cell("G1").Value = "Seat Number";
        worksheet.Cell("H1").Value = "Accompanying Seat Number (if couples seat)";
    }

    private static void AddBody(IXLWorksheet worksheet, IEnumerable<EventAttendeeDto> attendees)
    {
        var row = 2;

        foreach (var attendee in attendees)
        {
            worksheet.Cell(row, 1).Value = (row - 1); // set the serial number of the data 
            worksheet.Cell(row, 2).Value = attendee.FirstName;
            worksheet.Cell(row, 3).Value = attendee.LastName;
            worksheet.Cell(row, 4).Value = attendee.Phone;
            worksheet.Cell(row, 5).Value = attendee.Venue;
            worksheet.Cell(row, 6).Value = attendee.Category.ToString();
            worksheet.Cell(row, 7).Value = attendee.Seat;
            worksheet.Cell(row, 8).Value = attendee.AccompanyingSeat ?? "N/A";

            row++;
        }
    }

    private static byte[] WorkbookToByteArray(IXLWorkbook workbook)
    {
        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }
}