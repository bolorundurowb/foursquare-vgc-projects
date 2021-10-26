using System.Threading.Tasks;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using meerkat;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations
{
    public class PersonsRepository : IPersonsRepository
    {
        public Task<Person> GetByPhone(string phoneNumber)
        {
            phoneNumber = phoneNumber?.Normalize().Trim();
            return Meerkat.FindOneAsync<Person>(x => x.Phone == phoneNumber);
        }

        public async Task<Person> Create(string firstName, string lastName, string phoneNumber)
        {
            var person = await GetByPhone(phoneNumber);

            if (person == null)
            {
                person = new Person(firstName, lastName, phoneNumber);
                await person.SaveAsync();
            }

            return person;
        }
    }
}