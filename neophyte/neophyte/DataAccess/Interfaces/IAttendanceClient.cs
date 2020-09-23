using System.Threading.Tasks;
using neophyte.Models.Binding;
using neophyte.Models.View;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface IAttendanceClient
    {
        [Get("/attendance")]
        [Headers("Authorization: Bearer")]
        Task<DateSummaryViewModel[]> Get();
        
        [Post("/attendance")]
        Task Register([Body] AttendeeBindingModel payload);
        
        [Get("/attendance/{date}")]
        [Headers("Authorization: Bearer")]
        Task<AttendeeViewModel[]> GetAttendanceForDate(string date);
        
        [Put("/attendance/{id}")]
        [Headers("Authorization: Bearer")]
        Task<AttendeeViewModel> Update(string id, [Body] AttendeeUpdateBindingModel payload);
        
        [Delete("/attendance/{id}")]
        [Headers("Authorization: Bearer")]
        Task DeleteAttendee(string id);
    }
}