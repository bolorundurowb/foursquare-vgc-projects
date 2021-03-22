using System.Collections.Generic;
using System.IO;
using System.Linq;
using api.Data.Models;
using ClosedXML.Excel;

namespace api.Utils
{
    public static class ExcelGenerator
    {
        public static byte[] FromNewcomers(IEnumerable<Newcomer> newcomers)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet 1");
                // add header
                AddHeader(worksheet);

                var row = 1;
                foreach (var newcomer in newcomers)
                {
                    worksheet.Cell(row + 1, 1).Value = row;
                    worksheet.Cell(row + 1, 2).Value = newcomer.FullName;
                    worksheet.Cell(row + 1, 3).Value = newcomer.EmailAddress;
                    worksheet.Cell(row +1, 4).Value = newcomer.Phone;
                    worksheet.Cell(row + 1, 5).Value = newcomer.HomeAddress;
                    worksheet.Cell(row + 1, 6).Value = newcomer.BornAgain.GetValueOrDefault();
                    worksheet.Cell(row + 1, 7).Value = newcomer.BecomeMember.GetValueOrDefault().ToString();
                    worksheet.Cell(row + 1, 8).Value = newcomer.AgeGroup;
                    worksheet.Cell(row + 1, 9).Value = newcomer.BirthDay;
                    worksheet.Cell(row + 1, 10).Value = newcomer.CommentsOrPrayers;
                    worksheet.Cell(row + 1, 11).Value = newcomer.HowYouFoundUs;
                    worksheet.Cell(row + 1, 12).Value = newcomer.Remarks;
                    
                    // increement row counter
                    row++;
                }

                return ConvertWorkSheetToByteArray(workbook);
            }

            static void AddHeader(IXLWorksheet worksheet)
            {
                worksheet.Range(1, 1, 1, 13).Style.Font.Bold = true;
                worksheet.Cell(1,1).Value = "S/No";
                worksheet.Cell(1,2).Value = "Full Name";
                worksheet.Cell(1,3).Value = "Email Address";
                worksheet.Cell(1,4).Value = "Phone Number";
                worksheet.Cell(1,5).Value = "Residential Address";
                worksheet.Cell(1,6).Value = "Are you born again?";
                worksheet.Cell(1,7).Value = "Do you want to become a member of this church?";
                worksheet.Cell(1,8).Value = "Age Group";
                worksheet.Cell(1,9).Value = "Birthday";
                worksheet.Cell(1,10).Value = "Comments/Prayers";
                worksheet.Cell(1,11).Value = "How did you find out about us?";
                worksheet.Cell(1,12).Value = "Remarks";
            }
        }

        static byte[] ConvertWorkSheetToByteArray(IXLWorkbook workbook)
        {
            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms.ToArray();
        }
    }
}