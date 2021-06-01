using System.Threading.Tasks;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly DbContext _dbContext;

        public PersonsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMongoQueryable<Person> Query()
        {
            return _dbContext.Persons
                .AsQueryable();
        }

        public Task<Person> GetByPhone(string phoneNumber)
        {
            var trimmed = phoneNumber?.Trim();
            return Query()
                .FirstOrDefaultAsync(x => x.Phone == trimmed);
        }

        public async Task<Person> Create(string firstName, string lastName, string phoneNumber)
        {
            var person = new Person(firstName, lastName, phoneNumber);
            await _dbContext.Persons
                .InsertOneAsync(person);

            return person;
        }
    }
}