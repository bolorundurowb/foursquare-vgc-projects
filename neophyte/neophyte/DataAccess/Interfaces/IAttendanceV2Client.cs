﻿using System.Threading.Tasks;
using neophyte.Models.Binding;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface IAttendanceV2Client
    {
        [Post("/attendance")]
        Task Register([Body] AttendeeV2BindingModel payload);
    }
}