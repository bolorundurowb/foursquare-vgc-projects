﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.DTOs;
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

        public Task<List<DateSummaryDto>> GetNewcomersDates()
        {
            return Query()
                .GroupBy(x => x.Date)
                .Select(x => new DateSummaryDto
                {
                    Date = x.Key,
                    NumOfEntries = x.Count()
                })
                .OrderByDescending(x => x.Date)
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
            var newcomer = new Newcomer(fullName, homeAddress, phone, email, birthDay, gender, ageGroup,
                commentsOrPrayers, howYouFoundUs, bornAgain, becomeMember, remarks);
            await _dbContext.Newcomers
                .InsertOneAsync(newcomer);

            return newcomer;
        }

        public async Task<Newcomer> UpdateNewcomer(string id, DateTime? date, string fullName, string homeAddress,
            string phone, string email, string birthDay, Gender? gender, string ageGroup, string commentsOrPrayers,
            string howYouFoundUs, MultiChoice? bornAgain, MultiChoice? becomeMember, string remarks)
        {
            var newcomerId = ObjectId.Parse(id);
            var newcomer = await Query()
                .FirstOrDefaultAsync(x => x.Id == newcomerId);

            if (newcomer == null)
            {
                throw new NotFoundException("Newcomer not found.");
            }

            newcomer.UpdateDate(date);
            newcomer.UpdateFullName(fullName);
            newcomer.UpdateHomeAddress(homeAddress);
            newcomer.UpdatePhone(phone);
            newcomer.UpdateEmail(email);
            newcomer.UpdateBirthDay(birthDay);
            newcomer.UpdateGender(gender);
            newcomer.UpdateAgeGroup(ageGroup);
            newcomer.UpdateCommentsOrPrayers(commentsOrPrayers);
            newcomer.UpdateHowYouFoundUs(howYouFoundUs);
            newcomer.UpdateBornAgain(bornAgain);
            newcomer.UpdateBecomeMember(becomeMember);
            newcomer.UpdateRemarks(remarks);

            await _dbContext.Newcomers
                .ReplaceOneAsync(x => x.Id == newcomerId, newcomer);

            return newcomer;
        }

        public async Task RemoveNewcomer(string id)
        {
            var newcomerId = ObjectId.Parse(id);
            await _dbContext.Newcomers.DeleteOneAsync(x => x.Id == newcomerId);
        }
    }
}
