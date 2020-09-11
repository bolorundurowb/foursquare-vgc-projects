using System.Linq;
using api.Data.Models;
using MongoDB.Driver;

namespace api.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void SeedDefaults(this DbContext context)
        {
            var adminEmails = new[]
            {
                "carol_okpei@gmail.com", "jummya2002@yahoo.co.uk", "ogatimo@gmail.com", "olufemiolagunju@yahoo.com",
                "overcommeregegba@gmail.com", "tab@foursquarevgc.org"
            };

            foreach (var adminEmail in adminEmails)
            {
                var exists = context.Admins
                    .AsQueryable()
                    .Any(x => x.EmailAddress == adminEmail);

                if (!exists)
                {
                    var admin = new Admin(string.Empty, adminEmail);
                    context.Admins.InsertOne(admin);
                }
            }
        }
    }
}
