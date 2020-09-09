using api.Data.Models;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IMongoQueryable<T> Query();
    }
}