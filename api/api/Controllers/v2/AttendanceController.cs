using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v2
{
    [ApiVersion("2.0")]
    public class AttendanceController : ApiController
    {
        private readonly IAttendanceRepository _attendanceRepo;

        public AttendanceController(IMapper mapper, IAttendanceRepository attendanceRepo) :
            base(mapper)
        {
            _attendanceRepo = attendanceRepo;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(AttendeeViewModel), 201)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        [ProducesResponseType(typeof(GenericViewModel), 409)]
        public async Task<IActionResult> AddAttendee([FromBody] AttendeeRegistrationV2BindingModel bm)
        {
            var attendee = await _attendanceRepo.AddAttendee(bm.PersonId, bm.SeatNumber);
            return Created(Mapper.Map<AttendeeViewModel>(attendee));
        }
    }
}