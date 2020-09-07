using api.Data.Models;
using MongoDB.Driver;

namespace api.Data
{
    public class DbContext
    {
        public IMongoCollection<Newcomer> Newcomers { get; private set; }

        public IMongoCollection<Attendee> Attendance { get; private set; }

        public DbContext(string dbUrl, string dbName)
        {
            var client = new MongoClient(dbUrl);
            var database = client.GetDatabase(dbName);

            // set the queryables
            Newcomers = database.GetCollection<Newcomer>("newcomers");
            Attendance = database.GetCollection<Attendee>("attendance");
        }
    }
}