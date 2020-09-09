using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Enums;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendee>
    {
        Task<List<Attendee>> GetAttendees(DateTime date);
        
        Task<Attendee> AddAttendee(string fullName, string homeAddress, string phone, string email, string birthDay,
            Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? bornAgain,
            MultiChoice? becomeMember, string remarks);
        
        Task<Attendee> UpdateAttendee(string id, DateTime? date, string fullName, string homeAddress, string phone, string email, string birthDay,
            Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? bornAgain,
            MultiChoice? becomeMember, string remarks);
        
        Task RemoveAttendee(string id);
    }
}