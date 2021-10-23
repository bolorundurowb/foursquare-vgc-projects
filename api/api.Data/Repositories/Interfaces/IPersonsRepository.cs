using System.Threading.Tasks;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface IPersonsRepository
    {
        Task<Person> GetByPhone(string phoneNumber);

        Task<Person> Create(string firstName, string lastName, string phoneNumber);
    }
}