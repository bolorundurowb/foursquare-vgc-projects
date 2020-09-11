using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Enums;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendee>
    {
        Task<List<DateTime>> GetAttendanceDates();

        Task<List<Attendee>> GetAttendance(DateTime date);

        Task<Attendee> GetAttendeeById(string id);

        Task<Attendee> AddAttendee(string fullName, string email, int? age, string phone, string residentialAddress,
            Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers, bool caredForSickPerson,
            MultiChoice? haveCovidSymptoms);

        Task<Attendee> UpdateAttendee(string id, DateTime? date, string fullName, string email, int? age, string phone,
            string residentialAddress, Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers,
            bool caredForSickPerson, MultiChoice? haveCovidSymptoms, int? seatNumber);

        Task RemoveAttendee(string id);
    }
}
