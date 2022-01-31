using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Repositories.Interfaces;
using meerkat;

namespace api.Data.Repositories.Implementations;

public class AdminsRepository : IAdminsRepository
{
    public Task<Admin> FindById(string adminId) => Meerkat.FindByIdAsync<Admin>(adminId);

    public Task<Admin> FindByEmail(string email)
    {
        var normalizedEmail = email?.ToLowerInvariant();
        return Meerkat.FindOneAsync<Admin>(x => x.EmailAddress == normalizedEmail);
    }
}