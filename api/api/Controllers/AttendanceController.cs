using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Validators;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("v1/attendance")]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceRepository _attendanceRepo;
        
        public AttendanceController(IMapper mapper, IAttendanceRepository attendanceRepo) : base(mapper)
        {
            _attendanceRepo = attendanceRepo;
        }
        
        [HttpGet("dates")]
        [ProducesResponseType(typeof(IEnumerable<DateTime>), 200)]
        public async Task<IActionResult> GetAttendanceDates()
        {
            var dates = await _attendanceRepo.GetAttendanceDates();
            return Ok(dates);
        }

        [HttpGet("{date:DateTime}")]
        [ProducesResponseType(typeof(IEnumerable<AttendeeViewModel>), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        public async Task<IActionResult> GetAttendanceForDate(DateTime date)
        {
            var attendees = await _attendanceRepo.GetAttendance(date);
            return Ok(Mapper.Map<IEnumerable<AttendeeViewModel>>(attendees));
        }
    }
}
