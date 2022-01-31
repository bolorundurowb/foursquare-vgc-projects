using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Repositories.Interfaces;
using api.Shared.Exceptions;
using meerkat;

namespace api.Data.Repositories.Implementations;

public class AdminsRepository : IAdminsRepository
{
    public async Task<Admin> Login(string email)
    {
        var admin = await FindByEmail(email);

        if (admin == null)
            throw new NotFoundException("Admin not found.");

        return admin;
    }

    public Task<Admin> FindByEmail(string email)
    {
        var normalizedEmail = email?.ToLowerInvariant();
        return Meerkat.FindOneAsync<Admin>(x => x.EmailAddress == normalizedEmail);
    }
}