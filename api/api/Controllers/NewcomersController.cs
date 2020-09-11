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
    [Route("v1/newcomers")]
    public class NewcomersController : BaseController
    {
        private readonly INewcomersRepository _newcomersRepo;

        public NewcomersController(IMapper mapper, INewcomersRepository newcomersRepo) : base(mapper)
        {
            _newcomersRepo = newcomersRepo;
        }

        [HttpGet("dates")]
        [ProducesResponseType(typeof(IEnumerable<DateTime>), 200)]
        public async Task<IActionResult> GetNewcomerDates()
        {
            var dates = await _newcomersRepo.GetNewcomersDates();
            return Ok(dates);
        }

        [HttpGet("by-date")]
        [ProducesResponseType(typeof(IEnumerable<NewcomerViewModel>), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        public async Task<IActionResult> GetNewcomers([FromQuery] EntityQueryModel qm)
        {
            var (isValid, errorMessages) = await IsValid<EntityQueryModelValidator>(qm);
            if (!isValid)
            {
                return BadRequest(errorMessages);
            }

            var attendees = await _newcomersRepo.GetNewcomers(qm.Date);
            return Ok(Mapper.Map<IEnumerable<NewcomerViewModel>>(attendees));
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(NewcomerViewModel), 201)]
        public async Task<IActionResult> AddNewcomer([FromBody] NewcomerRegistrationBindingModel bm)
        {
            var attendee = await _newcomersRepo.AddNewcomer(bm.FullName, bm.HomeAddress, bm.Phone, bm.EmailAddress,
                bm.BirthDay, bm.Gender, bm.AgeGroup, bm.CommentsOrPrayers, bm.HowYouFoundUs, bm.BornAgain,
                bm.BecomeMember, bm.Remarks);
            return Created(Mapper.Map<NewcomerViewModel>(attendee));
        }

        [HttpPut("{id:string}")]
        [ProducesResponseType(typeof(NewcomerViewModel), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 404)]
        public async Task<IActionResult> UpdateNewcomer(string id, [FromBody] NewcomerRegistrationUpdateBindingModel bm)
        {
            try
            {
                var attendee = await _newcomersRepo.UpdateNewcomer(id, bm.Date, bm.FullName, bm.HomeAddress, bm.Phone,
                    bm.EmailAddress, bm.BirthDay, bm.Gender, bm.AgeGroup, bm.CommentsOrPrayers, bm.HowYouFoundUs,
                    bm.BornAgain, bm.BecomeMember, bm.Remarks);
                return Ok(Mapper.Map<NewcomerViewModel>(attendee));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:string}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveNewcomer(string id)
        {
            await _newcomersRepo.RemoveNewcomer(id);
            return Ok();
        }
    }
}
