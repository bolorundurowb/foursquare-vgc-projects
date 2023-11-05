using System.Collections.Generic;
using System.Threading.Tasks;
using neophyte.api.Data.Entities;

namespace neophyte.api.Data.Repositories.Interfaces;

public interface IPersonsRepository
{
    Task<List<Person>> GetAll(string? name);

    Task<Person> FindById(string personId);

    Task<Person?> GetByPhone(string phoneNumber);

    Task<Person> Create(string firstName, string lastName, string phoneNumber);
}