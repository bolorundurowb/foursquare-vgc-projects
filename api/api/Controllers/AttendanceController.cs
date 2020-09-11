using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Shared.Exceptions;
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
        private readonly INewcomersRepository _newcomersRepo;

        public AttendanceController(IMapper mapper, INewcomersRepository newcomersRepo) : base(mapper)
        {
            _newcomersRepo = newcomersRepo;
        }

        [HttpGet("dates")]
        [ProducesResponseType(typeof(IEnumerable<DateTime>), 200)]
        public async Task<IActionResult> AddAttendeeDates()
        {
            var dates = await _newcomersRepo.GetAttendanceDates();
            return Ok(dates);
        }

        [HttpGet("by-date")]
        [ProducesResponseType(typeof(IEnumerable<AttendeeViewModel>), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        public async Task<IActionResult> AddAttendee([FromQuery] AttendeesQueryModel qm)
        {
            var (isValid, errorMessages) = await IsValid<AttendeesQueryModelValidator>(qm);
            if (!isValid)
            {
                return BadRequest(errorMessages);
            }

            var attendees = await _newcomersRepo.GetAttendees(qm.Date);
            return Ok(Mapper.Map<IEnumerable<AttendeeViewModel>>(attendees));
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(AttendeeViewModel), 201)]
        public async Task<IActionResult> AddAttendee([FromBody] AttendeeRegistrationBindingModel bm)
        {
            var attendee = await _newcomersRepo.AddAttendee(bm.FullName, bm.HomeAddress, bm.Phone, bm.EmailAddress,
                bm.BirthDay, bm.Gender, bm.AgeGroup, bm.CommentsOrPrayers, bm.HowYouFoundUs, bm.BornAgain,
                bm.BecomeMember, bm.Remarks);
            return Created(Mapper.Map<AttendeeViewModel>(attendee));
        }

        [HttpPut("{id:string}")]
        [ProducesResponseType(typeof(AttendeeViewModel), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 404)]
        public async Task<IActionResult> UpdateAttendee(string id, [FromBody] AttendeeRegistrationUpdateBindingModel bm)
        {
            try
            {
                var attendee = await _newcomersRepo.UpdateAttendee(id, bm.Date, bm.FullName, bm.HomeAddress, bm.Phone,
                    bm.EmailAddress, bm.BirthDay, bm.Gender, bm.AgeGroup, bm.CommentsOrPrayers, bm.HowYouFoundUs,
                    bm.BornAgain, bm.BecomeMember, bm.Remarks);
                return Ok(Mapper.Map<AttendeeViewModel>(attendee));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:string}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveAttendee(string id)
        {
            await _newcomersRepo.RemoveAttendee(id);
            return Ok();
        }
    }
}