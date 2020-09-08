using System.Threading.Tasks;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using api.Shared.Exceptions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly DbContext _dbContext;

        public AdminsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMongoQueryable<Admin> Query()
        {
            return _dbContext.Admins
                .AsQueryable();
        }

        public async Task<Admin> Add(Admin entity)
        {
            await _dbContext.Admins
                .InsertOneAsync(entity);

            return entity;
        }

        public Task Commit(Admin entity)
        {
            return _dbContext.Admins
                .ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<Admin> Login(string email)
        {
            var normalizedEmail = email?.ToLowerInvariant();
            var admin = await Query()
                .FirstOrDefaultAsync(x => x.EmailAddress == normalizedEmail);

            if (admin == null)
            {
                throw new NotFoundException("Admin not found.");
            }

            return admin;
        }
    }
}