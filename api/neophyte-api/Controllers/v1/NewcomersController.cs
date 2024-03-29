﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Data.Generators;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;
using neophyte.api.Shared.Email.Interfaces;
using neophyte.api.Shared.Email.Models;
using neophyte.api.Shared.Exceptions;

namespace neophyte.api.Controllers.v1;

[ApiVersion("1.0")]
public class NewcomersController : ApiController
{
    private readonly INewcomersRepository _newcomersRepo;
    private readonly IEmailService _emailService;

    public NewcomersController(IMapper mapper, INewcomersRepository newcomersRepo, IEmailService emailService) :
        base(mapper)
    {
        _newcomersRepo = newcomersRepo;
        _emailService = emailService;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<DateSummaryViewModel>), 200)]
    public async Task<IActionResult> GetNewcomerDates()
    {
        var dateSummary = await _newcomersRepo.GetNewcomersDates();
        return Ok(Mapper.Map<IEnumerable<DateSummaryViewModel>>(dateSummary));
    }

    [HttpGet("{date:DateTime}")]
    [ProducesResponseType(typeof(IEnumerable<NewcomerViewModel>), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 400)]
    public async Task<IActionResult> GetNewcomersForDate(DateTime date)
    {
        var newcomers = await _newcomersRepo.GetNewcomers(date);
        return Ok(Mapper.Map<IEnumerable<NewcomerViewModel>>(newcomers));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(NewcomerViewModel), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    public async Task<IActionResult> UpdateNewcomer(string id, [FromBody] NewcomerUpdateBindingModel bm)
    {
        try
        {
            var newcomer = await _newcomersRepo.UpdateNewcomer(id, bm.Date, bm.FullName, bm.HomeAddress, bm.Phone,
                bm.EmailAddress, bm.BirthDay, bm.Gender, bm.AgeGroup, bm.CommentsOrPrayers, bm.HowYouFoundUs,
                bm.BornAgain, bm.BecomeMember, bm.Remarks);
            return Ok(Mapper.Map<NewcomerViewModel>(newcomer));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveNewcomer(string id)
    {
        await _newcomersRepo.RemoveNewcomer(id);
        return Ok();
    }

    [HttpPost("{date:DateTime}/send-report")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> GenerateReportForDate(DateTime date, [FromBody] ReportGenBindingModel bm)
    {
        var formattedDateString = date.Date.ToString("yyyy-MM-dd");
        var newcomers = await _newcomersRepo.GetNewcomers(date);
        var newcomerCsv = await CsvGenerator.ForNewcomers(newcomers);

        var emailMessage = new EmailMessage
        {
            Subject = $"Newcomer Reports For {formattedDateString}",
            Content = "<p>See attached for the generated report</p>",
            Attachments = new List<EmailAttachment>
            {
                new()
                {
                    Content = newcomerCsv,
                    MimeType = "text/csv",
                    Name = $"{formattedDateString}.csv"
                }
            }
        };
        await _emailService.SendAsync(bm?.EmailAddress, emailMessage);

        return NoContent();
    }
}