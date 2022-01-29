using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Repositories.Interfaces;
using api.Shared.Extensions;
using meerkat;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations;

public class PersonsRepository : IPersonsRepository
{
    public Task<List<Person>> GetAll(string name = null)
    {
        var queryable = Meerkat.Query<Person>();

        if (!string.IsNullOrWhiteSpace(name))
            queryable = (IMongoQueryable<Person>)Queryable.Where(queryable,
                x => x.FirstName.Contains(name) || x.LastName.Contains(name));

        return queryable.ToListAsync();
    }

    public Task<Person> FindById(string personId) => Meerkat.FindByIdAsync<Person>(personId);

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