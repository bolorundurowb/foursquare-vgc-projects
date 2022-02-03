using System;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;
using neophyte.api.Shared.Exceptions;

namespace neophyte.api.Controllers.v2;

[ApiVersion("2.0")]
[Obsolete("Attendance is now event based")]
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
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    [ProducesResponseType(typeof(GenericViewModel), 409)]
    public async Task<IActionResult> AddAttendee([FromBody] AttendeeRegistrationV2BindingModel bm)
    {
        try
        {
            var attendee = await _attendanceRepo.AddAttendee(bm.PersonId, bm.SeatAssigned, bm.SeatType);
            return Created(Mapper.Map<AttendeeViewModel>(attendee));
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}