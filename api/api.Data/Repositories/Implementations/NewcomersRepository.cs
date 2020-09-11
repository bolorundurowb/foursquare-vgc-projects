using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Enums;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using api.Shared.Exceptions;
using moment.net;
using moment.net.Enums;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations
{
    public class NewcomersRepository : INewcomersRepository
    {
        private readonly DbContext _dbContext;

        public NewcomersRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMongoQueryable<Newcomer> Query()
        {
            return _dbContext.Newcomers
                .AsQueryable();
        }

        public Task<List<DateTime>> GetNewcomersDates()
        {
            return Query()
                .Select(x => x.Date)
                .GroupBy(x => x.Date)
                .Select(x => x.Key)
                .ToListAsync();
        }

        public Task<List<Newcomer>> GetNewcomers(DateTime date)
        {
            var dayStart = date.StartOf(DateTimeAnchor.Day);
            var dayEnd = date.EndOf(DateTimeAnchor.Day);
            return Query()
                .Where(x => x.Date >= dayStart && x.Date < dayEnd)
                .ToListAsync();
        }

        public async Task<Newcomer> AddNewcomer(string fullName, string homeAddress, string phone, string email,
            string birthDay, Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs,
            MultiChoice? bornAgain, MultiChoice? becomeMember, string remarks)
        {
            var attendee = new Newcomer(fullName, homeAddress, phone, email, birthDay, gender, ageGroup,
                commentsOrPrayers, howYouFoundUs, bornAgain, becomeMember, remarks);
            await _dbContext.Newcomers
                .InsertOneAsync(attendee);

            return attendee;
        }

        public async Task<Newcomer> UpdateNewcomer(string id, DateTime? date, string fullName, string homeAddress,
            string phone, string email, string birthDay, Gender? gender, string ageGroup, string commentsOrPrayers,
            string howYouFoundUs, MultiChoice? bornAgain, MultiChoice? becomeMember, string remarks)
        {
            var attendeeId = ObjectId.Parse(id);
            var attendee = await Query()
                .FirstOrDefaultAsync(x => x.Id == attendeeId);

            if (attendee == null)
            {
                throw new NotFoundException("Newcomer not found.");
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

            await _dbContext.Newcomers
                .ReplaceOneAsync(x => x.Id == attendeeId, attendee);

            return attendee;
        }

        public async Task RemoveNewcomer(string id)
        {
            var attendeeId = ObjectId.Parse(id);
            await _dbContext.Newcomers.DeleteOneAsync(x => x.Id == attendeeId);
        }
    }
}
