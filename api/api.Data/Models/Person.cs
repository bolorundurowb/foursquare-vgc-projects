using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Data.Models
{
    public class Person : IEntity
    {
        [BsonId] 
        public ObjectId Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        private Person()
        {
        }

        public Person(string firstName, string lastName, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }
}