using System;

namespace api.Configuration
{
    public static class Config
    {
        public static string DbServerUrl => Environment.GetEnvironmentVariable("DB_SERVER");

        public static string DbName => Environment.GetEnvironmentVariable("DB_NAME");

        public static string Secret => Environment.GetEnvironmentVariable("SECRET");

        public static string DestinationEmail => Environment.GetEnvironmentVariable("DESTINATION_EMAIL");

        public static string Audience => "foursquare-neophyte";

        public static string Issuer => "foursquare-neophyte";
    }
}