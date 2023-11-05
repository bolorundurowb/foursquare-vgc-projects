using System;
using dotenv.net.Utilities;

namespace neophyte.api.Configuration;

public static class Config
{
    public static string DbUrl => EnvReader.GetStringValue("DB_URL");

    public static string Secret => EnvReader.GetStringValue("SECRET");

    public static string Audience => "neophyte";

    public static string Issuer => "neophyte";
}