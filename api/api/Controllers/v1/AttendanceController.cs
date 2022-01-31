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
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiVersion("1.0")]
[Obsolete("Attendance is now event based")]
public class AttendanceController : ApiController
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

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(NewcomerViewModel), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    public async Task<IActionResult> UpdateAttendance(string id, [FromBody] AttendanceUpdateBindingModel bm)
    {
        try
        {
            var attendee = await _attendanceRepo.UpdateAttendee(id, bm.Date, bm.FullName, bm.EmailAddress, bm.Age,
                bm.Phone, bm.ResidentialAddress, bm.Gender, bm.ReturnedInLastTenDays, bm.LiveWithCovidCaregivers,
                bm.CaredForSickPerson, bm.HaveCovidSymptoms, bm.SeatAssigned, bm.SeatType);
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
        await _emailService.SendAsync(bm?.EmailAddress, emailMessage);

        return NoContent();
    }
}