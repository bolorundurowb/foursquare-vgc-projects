using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Data.Models
{
    public class Attendee
    {
        [BsonId]
        public ObjectId Id { get; private set; }

        public DateTime Date { get; private set; }
    }
}