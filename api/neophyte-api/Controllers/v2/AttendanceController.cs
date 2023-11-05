using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Data.DTOs;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;

namespace neophyte.api.Controllers.v2;

[ApiVersion("2.0")]
public class AttendanceController : ApiController
{
    private readonly IAttendanceRegistryRepository _attendanceRegistryRepo;

    public AttendanceController(IMapper mapper, IAttendanceRegistryRepository attendanceRegistryRepo) : base(mapper) =>
        _attendanceRegistryRepo = attendanceRegistryRepo;

    [HttpGet("")]
    [ProducesResponseType(typeof(List<AttendanceSummaryDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] CollectionQueryModel qm)
    {
        var attendance = await _attendanceRegistryRepo.GetAll(qm.Skip, qm.Limit);
        return Ok(attendance);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(GenericViewModel), 400)]
    public async Task<IActionResult> Register([FromBody] RecordAttendeeBindingModel bm)
    {
        await _attendanceRegistryRepo.Create(bm.FirstName, bm.LastName, bm.EmailAddress, bm.PhoneNumber, bm.SeatNumber);
        return Created();
    }
}
