using System;
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

        public Task<List<DateSummaryDto>> GetAttendanceDates()
        {
            return Query()
                .Select(x => x.Date)
                .GroupBy(x => x.Date)
                .Select(x => new DateSummaryDto
                {
                    Date = x.Key,
                    NumOfEntries = x.Count()
                })
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public Task<List<Attendee>> GetAttendance(DateTime date)
        {
            var dayStart = date.StartOf(DateTimeAnchor.Day);
            var dayEnd = date.EndOf(DateTimeAnchor.Day);
            return Query()
                .Where(x => x.Date >= dayStart && x.Date < dayEnd)
                .ToListAsync();
        }

        public Task<Attendee> GetAttendeeById(string id)
        {
            var attendeeId = ObjectId.Parse(id);
            return Query()
                .FirstOrDefaultAsync(x => x.Id == attendeeId);
        }

        public async Task<Attendee> AddAttendee(string fullName, string email, int? age, string phone,
            string residentialAddress, Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers,
            bool caredForSickPerson, MultiChoice? haveCovidSymptoms)
        {
            var attendee = new Attendee(email, fullName, age, phone, residentialAddress, gender, returnedInLastTenDays,
                liveWithCovidCaregivers, caredForSickPerson, haveCovidSymptoms);
            await _dbContext.Attendance
                .InsertOneAsync(attendee);

            return attendee;
        }

        public async Task<Attendee> UpdateAttendee(string id, DateTime? date, string fullName, string email, int? age,
            string phone, string residentialAddress, Gender? gender, bool returnedInLastTenDays,
            bool liveWithCovidCaregivers, bool caredForSickPerson, MultiChoice? haveCovidSymptoms, int? seatNumber)
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
            attendee.UpdatePhone(phone);
            attendee.UpdateEmail(email);
            attendee.UpdateGender(gender);
            attendee.UpdateHaveCovidSymptoms(haveCovidSymptoms);
            attendee.UpdateAge(age);
            attendee.UpdateSeatNumber(seatNumber);
            attendee.UpdateReturnedInLastTenDays(returnedInLastTenDays);
            attendee.UpdateLiveWithCovidCaregivers(liveWithCovidCaregivers);
            attendee.UpdateCaredForSickPerson(caredForSickPerson);

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
