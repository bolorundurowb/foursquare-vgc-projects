using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Data.Models
{
    public class Admin : IEntity
    {
        [BsonId] public ObjectId Id { get; private set; }

        public string Name { get; private set; }

        public string EmailAddress { get; private set; }

        public DateTime AddedAt { get; private set; }

        private Admin()
        {
        }

        public Admin(string name, string emailAddress)
        {
            AddedAt = DateTime.UtcNow;
            Name = name;
            EmailAddress = emailAddress;
        }
    }
}