using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Repositories.Interfaces;
using api.Shared.Extensions;
using meerkat;

namespace api.Data.Repositories.Implementations
{
    public class PersonsRepository : IPersonsRepository
    {
        public Task<Person> GetByPhone(string phoneNumber)
        {
            phoneNumber = phoneNumber?.Regularize().Trim();
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