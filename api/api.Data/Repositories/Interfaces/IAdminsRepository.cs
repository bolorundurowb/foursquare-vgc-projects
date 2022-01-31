using System.Threading.Tasks;
using api.Data.Entities;

namespace api.Data.Repositories.Interfaces;

public interface IAdminsRepository
{
    Task<Admin> FindById(string adminId);

    Task<Admin> FindByEmail(string email);
}