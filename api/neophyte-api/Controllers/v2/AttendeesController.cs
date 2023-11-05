using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;

namespace neophyte.api.Controllers.v2;

[ApiVersion("2.0")]
public class AttendeesController : ApiController
{
    private readonly IAttendanceRegistryRepository _attendanceRegistryRepo;

    public AttendeesController(IMapper mapper, IAttendanceRegistryRepository attendanceRegistryRepo) : base(mapper) =>
        _attendanceRegistryRepo = attendanceRegistryRepo;

    [AllowAnonymous]
    [HttpPost("")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(GenericViewModel), 400)]
    public async Task<IActionResult> Create([FromBody] RecordAttendeeBindingModel bm)
    {
        await _attendanceRegistryRepo.Create(bm.FirstName, bm.LastName, bm.EmailAddress, bm.PhoneNumber, bm.SeatNumber);
        return Ok();
    }
}
