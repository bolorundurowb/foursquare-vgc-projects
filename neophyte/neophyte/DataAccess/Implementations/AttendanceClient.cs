using System;
using System.Threading.Tasks;
using neophyte.DataAccess.Interfaces;
using neophyte.Models.Binding;
using neophyte.Models.View;
using Refit;

namespace neophyte.DataAccess.Implementations
{
    public class AttendanceClient
    {
        private readonly IAttendanceClient _attendanceClient;

        public AttendanceClient()
        {
            var tokenClient = new TokenClient();
            _attendanceClient = RestService.For<IAttendanceClient>(Constants.BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task<DateSummaryViewModel[]> GetAll()
        {
            return _attendanceClient.Get();
        }

        public Task<AttendeeViewModel[]> GetAttendanceForDate(DateTime date)
        {
            return _attendanceClient.GetAttendanceForDate(date.ToString("yyyy-MM-dd"));
        }

        public Task Register([Body] AttendeeBindingModel payload)
        {
            return _attendanceClient.Register(payload);
        }

        public Task DeleteAttendee(string id)
        {
            return _attendanceClient.DeleteAttendee(id);
        }
    }
}