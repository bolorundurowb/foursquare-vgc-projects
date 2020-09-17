﻿using System.Threading.Tasks;
using neophyte.DataAccess.Interfaces;
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
    }
}