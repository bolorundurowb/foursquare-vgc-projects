using System.Threading.Tasks;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface IAdminsRepository : IRepository<Admin>
    {
        Task<Admin> Login(string email);
    }
}