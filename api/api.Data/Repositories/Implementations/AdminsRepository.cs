using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Repositories.Interfaces;
using api.Shared.Exceptions;
using meerkat;

namespace api.Data.Repositories.Implementations
{
    public class AdminsRepository : IAdminsRepository
    {
        public async Task<Admin> Login(string email)
        {
            var normalizedEmail = email?.ToLowerInvariant();
            var admin = await Meerkat.FindOneAsync<Admin>(x => x.EmailAddress == normalizedEmail);

            if (admin == null)
                throw new NotFoundException("Admin not found.");

            return admin;
        }
    }
}