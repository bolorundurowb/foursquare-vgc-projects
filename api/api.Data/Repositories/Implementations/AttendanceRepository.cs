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

        public async Task<Attendee> AddAttendee(string fullName, string email, int? age, string phone,
            string residentialAddress, Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers,
            bool caredForSickPerson, MultiChoice? haveCovidSymptoms, int? seatNumber)
        {
            var normalizedEmail = email?.ToLowerInvariant();
            var attendee = new Attendee(normalizedEmail, fullName, age, phone, residentialAddress, gender,
                returnedInLastTenDays, liveWithCovidCaregivers, caredForSickPerson, haveCovidSymptoms);

            if (seatNumber.HasValue)
            {
                attendee.UpdateSeatNumber(seatNumber);
            }

            if (!attendee.CanRegister())
            {
                throw new InvalidOperationException("You cannot reserve a seat at this time.");
            }

            await _dbContext.Attendance
                .InsertOneAsync(attendee);

            return attendee;
        }

        public async Task<Attendee> AddAttendee(string personId, string seatNumber, string seatType)
        {
            var today = DateTime.UtcNow.Date;
            var person = await _dbContext.Persons
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == ObjectId.Parse(personId));

            if (person == null)
            {
                throw new NotFoundException("User not pre-registered.");
            }

            var attendee = await Query()
                .FirstOrDefaultAsync(x => x.Phone == person.Phone && x.Date == today);

            if (attendee != null)
            {
                throw new ConflictException("Attendee is registered for today's service.");
            }

            attendee = new Attendee(person.FirstName, person.LastName, person?.Phone, seatNumber, seatType);
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
            attendee.UpdateResidentialAddress(residentialAddress);
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
