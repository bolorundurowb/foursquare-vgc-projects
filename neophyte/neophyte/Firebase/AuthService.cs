using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace neophyte.Firebase
{
    public class AuthService
    {
        public const string AuthKey = "Neophyte_IsLoggedIn";
        private const string AdminsKey = "admins";
        private const string FirebaseDatabaseUrl = "https://neophyte-648c7.firebaseio.com/";
        private static FirebaseClient _firebaseClientInstance;

        private static FirebaseClient DatabaseClient =>
            _firebaseClientInstance ?? (_firebaseClientInstance = new FirebaseClient(FirebaseDatabaseUrl));

        public async Task<bool> VerifyAdminEmail(string email)
        {
            var cleanedEmail = email
                .Trim()
                .Replace("@", "_")
                .Replace(".", "_");
            var adminEmailEntries = await DatabaseClient
                .Child(AdminsKey)
                .OrderByKey()
                .OnceAsync<object>();

            var adminEmails = adminEmailEntries?
                                  .Select(x => x.Key)
                                  .ToList() ?? new List<string>();

            return adminEmails.Any(x => x.Equals(cleanedEmail, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
