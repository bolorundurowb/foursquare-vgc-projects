using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Shared.Exceptions;
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

        [HttpPost("")]
        [ProducesResponseType(typeof(AttendeeViewModel), 201)]
        public async Task<IActionResult> AddAttendee([FromBody] AttendeeRegistrationBindingModel bm)
        {
            var attendee = await _attendanceRepo.AddAttendee(bm.FullName, bm.EmailAddress, bm.Age, bm.Phone,
                bm.ResidentialAddress, bm.Gender, bm.ReturnedInLastTenDays, bm.LiveWithCovidCaregivers,
                bm.CaredForSickPerson, bm.HaveCovidSymptoms);
            return Created(Mapper.Map<AttendeeViewModel>(attendee));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NewcomerViewModel), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 404)]
        public async Task<IActionResult> UpdateAttendance(string id, [FromBody] AttendanceUpdateBindingModel bm)
        {
            try
            {
                var attendee = await _attendanceRepo.UpdateAttendee(id, bm.Date, bm.FullName, bm.EmailAddress, bm.Age,
                    bm.Phone,
                    bm.ResidentialAddress, bm.Gender, bm.ReturnedInLastTenDays, bm.LiveWithCovidCaregivers,
                    bm.CaredForSickPerson, bm.HaveCovidSymptoms, bm.SeatNumber);
                return Ok(Mapper.Map<AttendeeViewModel>(attendee));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveAttendee(string id)
        {
            await _attendanceRepo.RemoveAttendee(id);
            return Ok();
        }
    }
}
