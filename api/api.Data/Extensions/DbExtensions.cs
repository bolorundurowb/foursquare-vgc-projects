using System.Linq;
using api.Data.Models;
using meerkat;
using MongoDB.Driver;

namespace api.Data.Extensions
{
    public static class DbExtensions
    {
        public static void SeedDefaults()
        {
            var adminEmails = new[]
            {
                "carol_okpei@gmail.com", "jummya2002@yahoo.co.uk", "ogatimo@gmail.com", "olufemiolagunju@yahoo.com",
                "overcommeregegba@gmail.com", "tab@foursquarevgc.org"
            };

            foreach (var adminEmail in adminEmails)
            {
                var admin = Meerkat.FindOne<Admin>(x => x.EmailAddress == adminEmail);

                if (admin != null)
                {
                    continue;
                }

                admin = new Admin(string.Empty, adminEmail);
                admin.Save();
            }
        }
    }
}