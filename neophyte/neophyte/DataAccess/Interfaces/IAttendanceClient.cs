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
    }
}