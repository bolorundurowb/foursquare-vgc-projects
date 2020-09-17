using System;
using System.Threading.Tasks;
using neophyte.DataAccess.Interfaces;
using Refit;

namespace neophyte.DataAccess.Implementations
{
    public class ReportClient
    {
        private readonly IReportClient _reportClient;

        public ReportClient()
        {
            var tokenClient = new TokenClient();
            _reportClient = RestService.For<IReportClient>(Constants.BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task GenerateReport(DateTime date)
        {
            return _reportClient.GenerateReport(date.ToString("yyyy-MM-dd"));
        }
    }
}