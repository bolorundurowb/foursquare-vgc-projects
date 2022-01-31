using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;

namespace api.Data.Repositories.Interfaces;

public interface IAdminsRepository
{
    Task<List<Admin>> GetAll();

    Task<Admin> FindById(string adminId);

    Task<Admin> FindByEmail(string email);

    Task UpdatePassword(Admin admin, string password);
}