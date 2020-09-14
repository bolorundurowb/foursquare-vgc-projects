using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Shared.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("v1/newcomers")]
    public class NewcomersController : BaseController
    {
        private readonly INewcomersRepository _newcomersRepo;

        public NewcomersController(IMapper mapper, INewcomersRepository newcomersRepo) : base(mapper)
        {
            _newcomersRepo = newcomersRepo;
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
            var attendees = await _newcomersRepo.GetNewcomers(date);
            return Ok(Mapper.Map<IEnumerable<NewcomerViewModel>>(attendees));
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(NewcomerViewModel), 201)]
        public async Task<IActionResult> AddNewcomer([FromBody] NewcomerBindingModel bm)
        {
            var attendee = await _newcomersRepo.AddNewcomer(bm.FullName, bm.HomeAddress, bm.Phone, bm.EmailAddress,
                bm.BirthDay, bm.Gender, bm.AgeGroup, bm.CommentsOrPrayers, bm.HowYouFoundUs, bm.BornAgain,
                bm.BecomeMember, bm.Remarks);
            return Created(Mapper.Map<NewcomerViewModel>(attendee));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NewcomerViewModel), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 404)]
        public async Task<IActionResult> UpdateNewcomer(string id, [FromBody] NewcomerUpdateBindingModel bm)
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

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveNewcomer(string id)
        {
            await _newcomersRepo.RemoveNewcomer(id);
            return Ok();
        }
    }
}
