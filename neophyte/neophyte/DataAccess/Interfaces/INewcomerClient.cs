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
        [Headers("Authorization: Bearer")]
        Task Register([Body] NewcomerBindingModel payload);

        [Put("/newcomers/{id}")]
        [Headers("Authorization: Bearer")]
        Task<NewcomerViewModel> Update(string id, [Body] NewcomerUpdateBindingModel payload);

        [Get("/newcomers/{date}")]
        [Headers("Authorization: Bearer")]
        Task<NewcomerViewModel[]> GetNewcomersForDate(string date);

        [Delete("/newcomers/{id}")]
        [Headers("Authorization: Bearer")]
        Task DeleteNewcomer(string id);

        [Post("/newcomers/{date}/send-report")]
        [Headers("Authorization: Bearer")]
        Task SendReport(string date, [Body] ReportGenBindingModel payload);
    }
}
