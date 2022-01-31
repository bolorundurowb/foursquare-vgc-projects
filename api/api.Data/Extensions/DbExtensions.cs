using System;
using System.Linq;
using api.Data.Entities;
using meerkat;
using dotenv.net.Utilities;

namespace api.Data.Extensions;

public static class DbExtensions
{
    public static void SeedDefaults()
    {
        EnvReader.TryGetStringValue("DEFAULT_ADMIN_EMAIL", out var adminEmail);
        EnvReader.TryGetStringValue("DEFAULT_ADMIN_PASS", out var adminPass);

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPass))
        {
            Console.WriteLine("Either the default admin email address or password is not provided. Skipping...");
            return;
        }

        var adminExists = Meerkat.Query<Admin>()
            .Any(x => x.EmailAddress == adminEmail);

        if (adminExists)
        {
            Console.WriteLine("A default admin already exists. Skipping...");
            return;
        }

        var admin = new Admin("Default Admin", adminEmail);
        admin.SetPassword(adminPass);
        admin.Save();
    }
}