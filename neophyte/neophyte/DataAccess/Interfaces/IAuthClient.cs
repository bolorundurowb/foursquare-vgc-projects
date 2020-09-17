using System.Threading.Tasks;
using neophyte.Models.Binding;
using neophyte.Models.View;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface IAuthClient
    {
        [Post("/auth/login")]
        Task<AuthViewModel> Login(LoginBindingModel payload);
    }
}