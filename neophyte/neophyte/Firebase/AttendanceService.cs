using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Offline;
using Firebase.Database.Query;
using neophyte.Models;

namespace neophyte.Firebase
{
    public class AttendanceService
    {
        private const string AttendanceKey = "attendance";
        private const string AttendanceBackupKey = "attendance-backup";
        private const string FirebaseDatabaseUrl = "https://neophyte-648c7.firebaseio.com/";
        private static FirebaseClient _firebaseClientInstance;

        private static FirebaseClient DatabaseClient =>
            _firebaseClientInstance ?? (_firebaseClientInstance = new FirebaseClient(FirebaseDatabaseUrl,
                new FirebaseOptions
                {
                    OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s)
                }));

        public async Task<List<DateEntry>> GetDayEntriesAsync()
        {
            var entries = await DatabaseClient
                .Child(AttendanceKey)
                .OrderByKey()
                .LimitToLast(25)
                .OnceAsync<object>();
            return entries?
                .OrderByDescending(x => x.Key)
                .Select(x => new DateEntry
                {
                    Date = x.Key
                })
                .ToList();
        }

        public async Task<List<Attendance>> GetAttendanceByDateAsync(string date)
        {
            var attendance = await DatabaseClient
                .Child(AttendanceKey)
                .Child(date)
                .OrderByKey()
                .OnceAsync<Attendance>();

            return attendance?
                .Select(x =>
                {
                    x.Object.AttendanceId = x.Key;
                    return x.Object;
                })
                .OrderBy(x => x.FullName)
                .ToList();
        }

        public async Task AddAttendanceAsync(Attendance attendance)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            await DatabaseClient
                .Child(AttendanceKey)
                .Child(date)
                .PostAsync(attendance);
            await DatabaseClient
                .Child(AttendanceBackupKey)
                .Child(date)
                .PostAsync(attendance);
        }

        public async Task DeleteAttendanceAsync(string date, string entryId)
        {
            await DatabaseClient
                .Child(AttendanceKey)
                .Child(date)
                .Child(entryId)
                .DeleteAsync();
        }

        public async Task<string> GenerateCsvForDateAsync(string date)
        {
            var attendances = await GetAttendanceByDateAsync(date);
            var csvBuilder = new StringBuilder();

            csvBuilder.Append("S/No");
            csvBuilder.Append('\t');
            csvBuilder.Append("Full Name");
            csvBuilder.Append('\t');
            csvBuilder.Append("Phone");
            csvBuilder.Append('\t');
            csvBuilder.Append("Email");
            csvBuilder.Append('\t');
            csvBuilder.Append("Residential Address");
            csvBuilder.Append('\t');
            csvBuilder.Append("Gender");
            csvBuilder.Append('\t');
            csvBuilder.Append("Age");
            csvBuilder.Append('\t');
            csvBuilder.Append("Did you return from a trip overseas in the last 10 days? Tick the applicable box");
            csvBuilder.Append('\t');
            csvBuilder.Append("Do you live with Covid-19 caregivers / health workers");
            csvBuilder.Append('\t');
            csvBuilder.Append("Did you recently care for any sick individual at home or in hospitals");
            csvBuilder.Append('\t');
            csvBuilder.Append("Do you presently have cold, cough, fever, sore throat, loss of smell or loss of appetite");
            csvBuilder.Append('\t');
            csvBuilder.Append("Seat Number");
            csvBuilder.Append(Environment.NewLine);

            for (var i = 0; i < attendances.Count; i++)
            {
                var attendance = attendances[i];

                csvBuilder.Append(i + 1);
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(attendance.FullName));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(attendance.Phone));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(attendance.EmailAddress));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(attendance.ResidentialAddress));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(attendance.Gender));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(attendance.Age));
                csvBuilder.Append('\t');
                csvBuilder.Append(attendance.ReturnedInLastTenDays ? "Yes" : "No");
                csvBuilder.Append('\t');
                csvBuilder.Append(attendance.LiveWithCovidCaregivers ? "Yes" : "No");
                csvBuilder.Append('\t');
                csvBuilder.Append(attendance.CaredForSickPerson ? "Yes" : "No");
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(attendance.HaveCovidSymptoms));
                csvBuilder.Append('\t');
                csvBuilder.Append(attendance.SeatNumber);
                csvBuilder.Append(Environment.NewLine);
            }

            return csvBuilder.ToString();
        }

        private static string CleanString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return " ";
            }

            return input
                .Replace('\n', ' ')
                .Replace('\t', ' ');
        }
    }
}
