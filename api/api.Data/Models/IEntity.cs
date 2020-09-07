using MongoDB.Bson;

namespace api.Data.Models
{
    public interface IEntity
    {
        ObjectId Id { get; }
    }
}