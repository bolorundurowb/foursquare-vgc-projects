using System;
using System.Threading.Tasks;
using neophyte.DataAccess.Interfaces;
using neophyte.Models.Binding;
using neophyte.Models.View;
using Refit;
using Xamarin.Essentials;

namespace neophyte.DataAccess.Implementations
{
    public class AuthClient
    {
        private const string AuthTokenKey = "Neophyte_Token";
        private const string AuthExpiryKey = "Neophyte_Expiry";
        private readonly IAuthClient _authClient;

        public AuthClient()
        {
            _authClient = RestService.For<IAuthClient>(Constants.BaseUrl);
        }

        public Task<AuthViewModel> Login(string email)
        {
            var bm = new LoginBindingModel
            {
                EmailAddress = email
            };
            return _authClient.Login(bm);
        }
        
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