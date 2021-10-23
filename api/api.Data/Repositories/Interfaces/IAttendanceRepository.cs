using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.DTOs;
using api.Data.Enums;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface IAttendanceRepository 
    {
        Task<List<DateSummaryDto>> GetAttendanceDates();

        Task<List<Attendee>> GetAttendance(DateTime date);

        Task<Attendee> AddAttendee(string fullName, string email, int? age, string phone, string residentialAddress,
            Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers, bool caredForSickPerson,
            MultiChoice? haveCovidSymptoms, int? seatNumber = null);

        Task<Attendee> AddAttendee(string personId, string seatNumber, string seatType);

        Task<Attendee> UpdateAttendee(string id, DateTime? date, string fullName, string email, int? age, string phone,
            string residentialAddress, Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers,
            bool caredForSickPerson, MultiChoice? haveCovidSymptoms, int? seatNumber);

        Task RemoveAttendee(string id);
    }
}
