using System;
using System.Threading.Tasks;
using neophyte.DataAccess.Interfaces;
using neophyte.Models.Binding;
using neophyte.Models.View;
using Refit;

namespace neophyte.DataAccess.Implementations
{
    public class NewcomerClient
    {
        private readonly INewcomerClient _newcomerClient;

        public NewcomerClient()
        {
            var tokenClient = new TokenClient();
            _newcomerClient = RestService.For<INewcomerClient>(Constants.BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }
        
        public Task<DateSummaryViewModel[]> GetAll()
        {
            return _newcomerClient.Get();
        }

        public Task<NewcomerViewModel[]> GetAttendanceForDate(DateTime date)
        {
            return _newcomerClient.GetNewcomersForDate(date.ToString("yyyy-MM-dd"));
        }

        public Task Register([Body] NewcomerBindingModel payload)
        {
            return _newcomerClient.Register(payload);
        }

        public Task DeleteAttendee(string id)
        {
            return _newcomerClient.DeleteAttendee(id);
        }
    }
}