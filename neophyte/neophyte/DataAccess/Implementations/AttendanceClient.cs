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
            _attendanceClient = RestService.For<IAttendanceClient>(Constants.V1BaseUrl, new RefitSettings
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

        public Task<AttendeeViewModel> Update(string id, [Body] AttendeeUpdateBindingModel payload)
        {
            return _attendanceClient.Update(id, payload);
        }

        public Task Register([Body] AttendeeBindingModel payload)
        {
            return _attendanceClient.Register(payload);
        }

        public Task DeleteAttendee(string id)
        {
            return _attendanceClient.DeleteAttendee(id);
        }

        public Task SendAttendanceReport(DateTime date, string email)
        {
            var bm = new ReportGenBindingModel
            {
                EmailAddress = email
            };
            return _attendanceClient.SendReport(date.ToString("yyyy-MM-dd"), bm);
        }
    }
}
