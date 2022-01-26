using System.Linq;
using api.Data.Entities;
using meerkat;
using dotenv.net.Utilities;

namespace api.Data.Extensions;

public static class DbExtensions
{
    public static void SeedDefaults()
    {
        EnvReader.TryGetStringValue("AUTH_EMAILS", out var adminEmails);
        var emailAddresses = adminEmails.Split(",")
            .Select(x => x.ToLowerInvariant().Trim());

        foreach (var adminEmail in emailAddresses)
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