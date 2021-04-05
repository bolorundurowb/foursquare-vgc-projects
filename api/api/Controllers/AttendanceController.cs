using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Configuration;
using api.Data.Helpers;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Shared.Email.Interfaces;
using api.Shared.Email.Models;
using api.Shared.Exceptions;
using api.Validators;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly IEmailService _emailService;

        public AttendanceController(IMapper mapper, IAttendanceRepository attendanceRepo, IEmailService emailService) :
            base(mapper)
        {
            _attendanceRepo = attendanceRepo;
            _emailService = emailService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<DateSummaryViewModel>), 200)]
        public async Task<IActionResult> GetAttendanceDates()
        {
            var dateSummary = await _attendanceRepo.GetAttendanceDates();
            return Ok(Mapper.Map<IEnumerable<DateSummaryViewModel>>(dateSummary));
        }

        [HttpGet("{date:DateTime}")]
        [ProducesResponseType(typeof(IEnumerable<AttendeeViewModel>), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        public async Task<IActionResult> GetAttendanceForDate(DateTime date)
        {
            var attendees = await _attendanceRepo.GetAttendance(date);
            return Ok(Mapper.Map<IEnumerable<AttendeeViewModel>>(attendees));
        }

        [AllowAnonymous]
        [HttpPost("")]
        [ProducesResponseType(typeof(AttendeeViewModel), 201)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        [ProducesResponseType(typeof(GenericViewModel), 409)]
        public async Task<IActionResult> AddAttendee([FromBody] AttendeeRegistrationBindingModel bm)
        {
            var (isValid, errorMessages) =
                await IsValid<AttendeeRegistrationBindingModelValidator, AttendeeRegistrationBindingModel>(bm);
            if (!isValid)
            {
                return BadRequest(errorMessages);
            }

            try
            {
                var attendee = await _attendanceRepo.AddAttendee(bm.FullName, bm.EmailAddress, bm.Age, bm.Phone,
                    bm.ResidentialAddress, bm.Gender, bm.ReturnedInLastTenDays, bm.LiveWithCovidCaregivers,
                    bm.CaredForSickPerson, bm.HaveCovidSymptoms, bm.SeatNumber);
                return Created(Mapper.Map<AttendeeViewModel>(attendee));
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpPost("{date:DateTime}/send-report")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GenerateReportForDate(DateTime date, [FromBody] ReportGenBindingModel bm)
        {
            var formattedDateString = date.Date.ToString("yyyy-MM-dd");
            var attendance = await _attendanceRepo.GetAttendance(date);
            
            // add in the serial numbers, since the csv mapper doesnt work
            var attendanceSerialNo = 1;
            attendance.ForEach(x =>
            {
                x.SerialNo = attendanceSerialNo;
                attendanceSerialNo++;
            });
            
            var attendanceCsv = await CsvHelpers.GenerateCsvFromAttendance(attendance);

            var emailMessage = new EmailMessage
            {
                Subject = $"Attendance Reports For {formattedDateString}",
                Content = "<p>See attached for the generated report</p>",
                Attachments = new List<EmailAttachment>
                {
                    new()
                    {
                        Content = attendanceCsv,
                        MimeType = "text/csv",
                        Name = $"{formattedDateString}.csv"
                    }
                }
            };
            await _emailService.SendAsync(bm?.EmailAddress ?? Config.DestinationEmail, emailMessage);

            return NoContent();
        }
    }
}
