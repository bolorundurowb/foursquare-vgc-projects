using System;
using Xamarin.Essentials;

namespace neophyte.DataAccess.Implementations
{
    public class TokenClient
    {
        private const string AuthTokenKey = "Neophyte_Token";
        private const string AuthExpiryKey = "Neophyte_Expiry";
        private const string AuthLoginKey = "Neophyte_Login";
        private const string AuthEmailKey = "Neophyte_Email";

        public void Logout()
        {
            Preferences.Clear(AuthExpiryKey);
            Preferences.Clear(AuthLoginKey);
            Preferences.Clear(AuthTokenKey);
        }

        public bool IsLoggedIn()
        {
            var token = GetToken();
            var expiresAt = Preferences.Get(AuthExpiryKey, DateTime.MinValue);

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            return expiresAt > DateTime.UtcNow;
        }

        public string GetToken()
        {
            return Preferences.Get(AuthTokenKey, null);
        }

        public string GetEmail()
        {
            return Preferences.Get(AuthEmailKey, null);
        }

        public DateTime GetExpiry()
        {
            return Preferences.Get(AuthExpiryKey, DateTime.MinValue);
        }

        public DateTime GetLogin()
        {
            return Preferences.Get(AuthLoginKey, DateTime.MinValue);
        }

        public void SetAuth(string emailAddress, string token, DateTime expiresAt)
        {
            Preferences.Set(AuthEmailKey, emailAddress);
            Preferences.Set(AuthTokenKey, token);
            Preferences.Set(AuthExpiryKey, expiresAt);
            Preferences.Set(AuthLoginKey, DateTime.UtcNow);
        }
    }
}
