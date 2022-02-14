using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meerkat;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Shared.Extensions;

namespace neophyte.api.Data.Repositories.Implementations;

public class PersonsRepository : IPersonsRepository
{
    public Task<List<Person>> GetAll(string name = null)
    {
        var queryable = Meerkat.Query<Person>();

        if (!string.IsNullOrWhiteSpace(name))
        {
            var filter = Builders<Person>.Filter.Or(
                new FilterDefinitionBuilder<Person>().Regex(x => x.FirstName,
                    new BsonRegularExpression($"^{Regex.Escape(name)}", "i")),
                new FilterDefinitionBuilder<Person>().Regex(x => x.LastName,
                    new BsonRegularExpression($"^{Regex.Escape(name)}", "i"))
            );
            queryable = queryable.Where(_ => filter.Inject());
            
        }

        return queryable.ToListAsync();
    }

    public Task<Person> FindById(string personId) => Meerkat.FindByIdAsync<Person>(ObjectId.Parse(personId));

    public Task<Person> GetByPhone(string phoneNumber)
    {
        phoneNumber = phoneNumber?.Regularize().Trim();
        return Meerkat.FindOneAsync<Person>(x => x.Phone == phoneNumber);
    }

    public async Task<Person> Create(string firstName, string lastName, string phoneNumber)
    {
        var person = await GetByPhone(phoneNumber);

        if (person != null)
            return person;

        person = new Person(firstName, lastName, phoneNumber);
        await person.SaveAsync();

        return person;
    }
}