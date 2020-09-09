using System;
using System.Threading.Tasks;
using api.Data.Enums;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using api.Shared.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DbContext _dbContext;

        public AttendanceRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMongoQueryable<Attendee> Query()
        {
            return _dbContext.Attendance
                .AsQueryable();
        }

        public async Task<Attendee> AddAttendee(string fullName, string homeAddress, string phone, string email,
            string birthDay, Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs,
            MultiChoice? bornAgain, MultiChoice? becomeMember, string remarks)
        {
            var attendee = new Attendee(fullName, homeAddress, phone, email, birthDay, gender, ageGroup,
                commentsOrPrayers, howYouFoundUs, bornAgain, becomeMember, remarks);
            await _dbContext.Attendance
                .InsertOneAsync(attendee);

            return attendee;
        }

        public async Task<Attendee> UpdateAttendee(string id, DateTime? date, string fullName, string homeAddress, string phone, string email,
            string birthDay, Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs,
            MultiChoice? bornAgain, MultiChoice? becomeMember, string remarks)
        {
            var attendeeId = ObjectId.Parse(id);
            var attendee = await Query()
                .FirstOrDefaultAsync(x => x.Id == attendeeId);

            if (attendee == null)
            {
                throw new NotFoundException("Attendee not found.");
            }
            
            
        }
    }
}