using System;
using System.Threading.Tasks;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface IReportClient
    {
        [Post("/reports/{date}")]
        [Headers("Authorization: Bearer")]
        Task GenerateReport(string date);
    }
}