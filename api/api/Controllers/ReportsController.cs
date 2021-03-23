using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Configuration;
using api.Data.Helpers;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Shared.Email.Interfaces;
using api.Shared.Email.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly INewcomersRepository _newcomersRepo;
        private readonly IEmailService _emailService;

        public ReportsController(IMapper mapper, IAttendanceRepository attendanceRepo,
            INewcomersRepository newcomersRepo, IEmailService emailService) : base(mapper)
        {
            _attendanceRepo = attendanceRepo;
            _newcomersRepo = newcomersRepo;
            _emailService = emailService;
        }

        [HttpPost("{date:DateTime}")]
        [ProducesResponseType(200)]
        [Obsolete("Use the entity specific methods")]
        public async Task<IActionResult> GenerateReports(DateTime date, [FromBody] ReportGenBindingModel bm)
        {
            var formattedDateString = date.Date.ToString("yyyy-MM-dd");
            var attendance = await _attendanceRepo.GetAttendance(date);
            var newcomers = await _newcomersRepo.GetNewcomers(date);

            var attendanceCsv = await CsvHelpers.GenerateCsvFromAttendance(attendance);
            var newcomerCsv = await CsvHelpers.GenerateCsvFromNewcomers(newcomers);

            var emailMessage = new EmailMessage
            {
                Subject = $"Reports for {formattedDateString}",
                Content = "<p>See attached for reports</p>",
                Attachments = new List<EmailAttachment>
                {
                    new()
                    {
                        Content = attendanceCsv,
                        MimeType = "text/csv",
                        Name = $"{formattedDateString}-attendance.csv"
                    },
                    new()
                    {
                        Content = newcomerCsv,
                        MimeType = "text/csv",
                        Name = $"{formattedDateString}-newcomers.csv"
                    }
                }
            };
            await _emailService.SendAsync(bm?.EmailAddress ?? Config.DestinationEmail, emailMessage);

            return Ok();
        }
    }
}