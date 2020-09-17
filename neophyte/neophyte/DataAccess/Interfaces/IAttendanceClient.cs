using System;
using System.Threading.Tasks;
using neophyte.Models.View;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface IAttendanceClient
    {
        [Get("/attendance")]
        [Headers("Authorization: Bearer")]
        Task<DateSummaryViewModel[]> Get();
        
        [Get("/attendance/{date}")]
        [Headers("Authorization: Bearer")]
        Task<AttendeeViewModel[]> GetAttendanceForDate(string date);
        
        [Delete("/attendance/{id}")]
        [Headers("Authorization: Bearer")]
        Task DeleteAttendee(string id);
    }
}