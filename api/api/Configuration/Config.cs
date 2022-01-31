using System;

namespace api.Configuration;

public static class Config
{
    public static string DbUrl => Environment.GetEnvironmentVariable("DB_URL");

    public static string Secret => Environment.GetEnvironmentVariable("SECRET");

    public static string Audience => "neophyte";

    public static string Issuer => "neophyte";
}