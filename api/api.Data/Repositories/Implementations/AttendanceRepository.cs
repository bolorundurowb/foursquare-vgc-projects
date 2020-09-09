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

        public async Task<Attendee> UpdateAttendee(string id, DateTime? date, string fullName, string homeAddress,
            string phone, string email, string birthDay, Gender? gender, string ageGroup, string commentsOrPrayers,
            string howYouFoundUs, MultiChoice? bornAgain, MultiChoice? becomeMember, string remarks)
        {
            var attendeeId = ObjectId.Parse(id);
            var attendee = await Query()
                .FirstOrDefaultAsync(x => x.Id == attendeeId);

            if (attendee == null)
            {
                throw new NotFoundException("Attendee not found.");
            }

            attendee.UpdateDate(date);
            attendee.UpdateFullName(fullName);
            attendee.UpdateHomeAddress(homeAddress);
            attendee.UpdatePhone(phone);
            attendee.UpdateEmail(email);
            attendee.UpdateBirthDay(birthDay);
            attendee.UpdateGender(gender);
            attendee.UpdateAgeGroup(ageGroup);
            attendee.UpdateCommentsOrPrayers(commentsOrPrayers);
            attendee.UpdateHowYouFoundUs(howYouFoundUs);
            attendee.UpdateBornAgain(bornAgain);
            attendee.UpdateBecomeMember(becomeMember);
            attendee.UpdateRemarks(remarks);

            await _dbContext.Attendance
                .ReplaceOneAsync(x => x.Id == attendeeId, attendee);

            return attendee;
        }

        public async Task RemoveAttendee(string id)
        {
            var attendeeId = ObjectId.Parse(id);
            await _dbContext.Attendance.DeleteOneAsync(x => x.Id == attendeeId);
        }
    }
}