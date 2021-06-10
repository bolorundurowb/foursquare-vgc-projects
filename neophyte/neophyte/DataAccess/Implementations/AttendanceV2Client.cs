using System.Threading.Tasks;
using neophyte.DataAccess.Interfaces;
using neophyte.Models.Binding;
using Refit;

namespace neophyte.DataAccess.Implementations
{
    public class AttendanceV2Client
    {
        private readonly IAttendanceV2Client _attendanceClient;

        public AttendanceV2Client()
        {
            var tokenClient = new TokenClient();
            _attendanceClient = RestService.For<IAttendanceV2Client>(Constants.V2BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(tokenClient.GetToken())
            });
        }

        public Task Register([Body] AttendeeV2BindingModel payload)
        {
            return _attendanceClient.Register(payload);
        }
    }
}
