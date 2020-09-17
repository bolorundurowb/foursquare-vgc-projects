using System.Threading.Tasks;
using neophyte.Models.Binding;
using neophyte.Models.View;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface INewcomerClient
    {
        [Get("/newcomers")]
        [Headers("Authorization: Bearer")]
        Task<DateSummaryViewModel[]> Get();
        
        [Post("/newcomers")]
        Task Register([Body] NewcomerBindingModel payload);
        
        [Get("/newcomers/{date}")]
        [Headers("Authorization: Bearer")]
        Task<NewcomerViewModel[]> GetNewcomersForDate(string date);
        
        [Delete("/newcomers/{id}")]
        [Headers("Authorization: Bearer")]
        Task DeleteAttendee(string id);
    }
}