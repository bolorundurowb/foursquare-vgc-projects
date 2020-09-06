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
    public class RecordService
    {
        private const string MembersKey = "members";
        private const string MembersBackupKey = "members-backup";
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
                .Child(MembersKey)
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

        public async Task<List<Record>> GetRecordByDateAsync(string date)
        {
            var records = await DatabaseClient
                .Child(MembersKey)
                .Child(date)
                .OrderByKey()
                .OnceAsync<Record>();

            return records?
                .Select(x =>
                {
                    x.Object.RecordId = x.Key;
                    return x.Object;
                })
                .OrderBy(x => x.FullName)
                .ToList();
        }

        public async Task CreateRecordAsync(Record record)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            await DatabaseClient
                .Child(MembersKey)
                .Child(date)
                .PostAsync(record);
            await DatabaseClient
                .Child(MembersBackupKey)
                .Child(date)
                .PostAsync(record);
        }

        public async Task DeleteRecordAsync(string date, string entryId)
        {
            await DatabaseClient
                .Child(MembersKey)
                .Child(date)
                .Child(entryId)
                .DeleteAsync();
        }

        public async Task UpdateRecordAsync(string date, string entryId, Record record)
        {
            await DatabaseClient
                .Child(MembersKey)
                .Child(date)
                .Child(entryId)
                .PutAsync(record);
        }

        public async Task<string> GenerateCsvForDateAsync(string date)
        {
            var records = await GetRecordByDateAsync(date);
            var csvBuilder = new StringBuilder();

            csvBuilder.Append("S/No");
            csvBuilder.Append('\t');
            csvBuilder.Append("Full Name");
            csvBuilder.Append('\t');
            csvBuilder.Append("Phone");
            csvBuilder.Append('\t');
            csvBuilder.Append("Email");
            csvBuilder.Append('\t');
            csvBuilder.Append("Home Address");
            csvBuilder.Append('\t');
            csvBuilder.Append("Birthday");
            csvBuilder.Append('\t');
            csvBuilder.Append("Age Group");
            csvBuilder.Append('\t');
            csvBuilder.Append("Gender");
            csvBuilder.Append('\t');
            csvBuilder.Append("How You Found About Us");
            csvBuilder.Append('\t');
            csvBuilder.Append("Comments/Prayer Points");
            csvBuilder.Append('\t');
            csvBuilder.Append("Are you born again");
            csvBuilder.Append('\t');
            csvBuilder.Append("Do You Want To Become A Member");
            csvBuilder.Append('\t');
            csvBuilder.Append("Remarks");
            csvBuilder.Append(Environment.NewLine);

            for (var i = 0; i < records.Count; i++)
            {
                var record = records[i];

                csvBuilder.Append(i + 1);
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.FullName));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.Phone));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.Email));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.HomeAddress));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.BirthDay));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.AgeGroup));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.Gender));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.HowYouFoundUs));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.CommentsOrPrayers));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.BornAgain));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.BecomeMember));
                csvBuilder.Append('\t');
                csvBuilder.Append(CleanString(record.Remarks));
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
