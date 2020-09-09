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
    [Microsoft.AspNetCore.Components.Route("v1/attendance")]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceRepository _attendanceRepo;

        public AttendanceController(IMapper mapper, IAttendanceRepository attendanceRepo) : base(mapper)
        {
            _attendanceRepo = attendanceRepo;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(AttendeeViewModel), 201)]
        public async Task<IActionResult> AddAttendee([FromBody] AttendeeRegistrationBindingModel bm)
        {
            var attendee = await _attendanceRepo.AddAttendee(bm.FullName, bm.HomeAddress, bm.Phone, bm.EmailAddress,
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
                var attendee = await _attendanceRepo.UpdateAttendee(id, bm.Date, bm.FullName, bm.HomeAddress, bm.Phone,
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
            await _attendanceRepo.RemoveAttendee(id);
            return Ok();
        }
    }
}