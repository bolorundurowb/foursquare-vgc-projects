using System.Threading.Tasks;
using neophyte.Models.Binding;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface IAuthClient
    {
        [Post("/auth/login")]
        Task<> Login(LoginBindingModel payload);
    }
}