using System.Threading.Tasks;
using neophyte.DataAccess.Interfaces;
using neophyte.Models.Binding;
using neophyte.Models.View;
using Refit;

namespace neophyte.DataAccess.Implementations
{
    public class AuthClient
    {
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
    }
}