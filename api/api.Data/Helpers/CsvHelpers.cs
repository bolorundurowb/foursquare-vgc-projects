using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using api.Data.CsvMappers;
using api.Data.Models;
using CsvHelper;

namespace api.Data.Helpers
{
    public static class CsvHelpers
    {
        public static async Task<byte[]> GenerateCsvFromAttendance(IEnumerable<Attendee> attendees)
        {
            await using var writer = new StringWriter();
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap<AttendeeCsvMapper>();
            await csv.WriteRecordsAsync(attendees);
            var csvString = writer.ToString();
            return Encoding.UTF8.GetBytes(csvString);
        }

        public static async Task<byte[]> GenerateCsvFromNewcomers(IEnumerable<Newcomer> newcomers)
        {
            await using var writer = new StringWriter();
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap<NewcomerCsvMapper>();
            await csv.WriteRecordsAsync(newcomers);
            var csvString = writer.ToString();
            return Encoding.UTF8.GetBytes(csvString);
        }
    }
}