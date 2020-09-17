using System;
using Xamarin.Essentials;

namespace neophyte.DataAccess.Implementations
{
    public class TokenClient
    {
        private const string AuthTokenKey = "Neophyte_Token";
        private const string AuthExpiryKey = "Neophyte_Expiry";

        public void Logout()
        {
            Preferences.Clear();
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

        public void SetAuth(string token, DateTime expiresAt)
        {
            Preferences.Set(AuthTokenKey, token);
            Preferences.Set(AuthExpiryKey, expiresAt);
        }
    }
}