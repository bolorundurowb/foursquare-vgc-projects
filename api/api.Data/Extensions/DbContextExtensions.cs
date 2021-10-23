using System.Linq;
using api.Data.Models;
using dotenv.net.Utilities;
using MongoDB.Driver;

namespace api.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void SeedDefaults(this DbContext context)
        {
            EnvReader.TryGetStringValue("AUTH_EMAILS", out var adminEmails);
            var emailAddresses = adminEmails.Split(",")
                .Select(x => x.ToLowerInvariant().Trim());

            foreach (var adminEmail in emailAddresses)
            {
                var exists = context.Admins
                    .AsQueryable()
                    .Any(x => x.EmailAddress == adminEmail);

                if (exists)
                {
                    continue;
                }
                
                var admin = new Admin(string.Empty, adminEmail);
                context.Admins.InsertOne(admin);
            }
        }
    }
}
