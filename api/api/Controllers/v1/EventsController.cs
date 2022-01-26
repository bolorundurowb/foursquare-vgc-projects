using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiVersion("1.0")]
public class EventsController : ApiController
{
    private readonly IEventRepository _eventRepo;

    public EventsController(IMapper mapper, IEventRepository eventRepo) : base(mapper)
    {
        _eventRepo = eventRepo;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<EventViewModel>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] CollectionQueryModel qm)
    {
        var events = await _eventRepo.GetAll(qm.Skip.Value, qm.Limit.Value);
        return Ok(Mapper.Map<List<EventViewModel>>(events));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(EventViewModel), 201)]
    [ProducesResponseType(typeof(GenericViewModel), 409)]
    public async Task<IActionResult> Create([FromBody] VenueCreationBindingModel bm)
    {
        var venue = await _eventRepo.FindByNameAndDate(bm.Name);

        if (venue != null)
            return Conflict("A venue exists with the same name.");

        var seatRanges = bm.SeatRanges
            .Select(x => (x.Category, x.Number))
            .ToList();
        venue = await _venueRepo.Create(bm.Name, seatRanges);
        return Ok(Mapper.Map<VenueViewModel>(venue));
    }
}