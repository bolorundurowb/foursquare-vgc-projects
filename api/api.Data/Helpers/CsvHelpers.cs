using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using api.Data.CsvMappers;
using api.Data.Entities;
using CsvHelper;

namespace api.Data.Helpers
{
    public static class CsvHelpers
    {
        public static async Task<byte[]> GenerateCsvFromAttendance(List<Attendee> attendees)
        {
            // add in the serial numbers, since the csv mapper doesnt work
            var attendanceSerialNo = 1;
            attendees.ForEach(x =>
            {
                x.SerialNo = attendanceSerialNo;
                attendanceSerialNo++;
            });

            await using var writer = new StringWriter();
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<AttendeeCsvMapper>();
            await csv.WriteRecordsAsync(attendees);
            var csvString = writer.ToString();
            return Encoding.UTF8.GetBytes(csvString);
        }

        public static async Task<byte[]> GenerateCsvFromNewcomers(List<Newcomer> newcomers)
        {
            // add in the serial numbers, since the csv mapper doesnt work
            var newcomerSerialNo = 1;
            newcomers.ForEach(x =>
            {
                x.SerialNo = newcomerSerialNo;
                newcomerSerialNo++;
            });

            await using var writer = new StringWriter();
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<NewcomerCsvMapper>();
            await csv.WriteRecordsAsync(newcomers);
            var csvString = writer.ToString();
            return Encoding.UTF8.GetBytes(csvString);
        }
    }
}
