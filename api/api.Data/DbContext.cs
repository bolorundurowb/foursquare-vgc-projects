using api.Data.Models;
using MongoDB.Driver;

namespace api.Data
{
    public class DbContext
    {
        public IMongoCollection<Admin> Admins { get; }

        public IMongoCollection<Newcomer> Newcomers { get; }

        public IMongoCollection<Attendee> Attendance { get; }

        public DbContext(string dbUrl, string dbName)
        {
            var client = new MongoClient(dbUrl);
            var database = client.GetDatabase(dbName);

            // set the queryables
            Admins = database.GetCollection<Admin>("admins");
            Newcomers = database.GetCollection<Newcomer>("newcomers");
            Attendance = database.GetCollection<Attendee>("attendance");
        }
    }
}