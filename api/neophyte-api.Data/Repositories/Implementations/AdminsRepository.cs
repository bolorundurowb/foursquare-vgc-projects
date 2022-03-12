using System.Collections.Generic;
using System.Threading.Tasks;
using meerkat;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Repositories.Interfaces;

namespace neophyte.api.Data.Repositories.Implementations;

public class AdminsRepository : IAdminsRepository
{
    public Task<List<Admin>> GetAll() => Meerkat.Query<Admin>()
        .OrderBy(x => x.Name)
        .ToListAsync();

    public Task<Admin> FindById(string adminId) => Meerkat.FindByIdAsync<Admin>(ObjectId.Parse(adminId));

    public Task<Admin> FindByEmail(string email)
    {
        var normalizedEmail = email?.ToLowerInvariant();
        return Meerkat.FindOneAsync<Admin>(x => x.EmailAddress == normalizedEmail);
    }

    public async Task<Admin> Create(string name, string email, string password)
    {
        var admin = new Admin(name, email);
        admin.SetPassword(password, true);
        await admin.SaveAsync();

        return admin;
    }

    public Task UpdatePassword(Admin admin, string password)
    {
        admin.SetPassword(password);
        return admin.SaveAsync();
    }
}