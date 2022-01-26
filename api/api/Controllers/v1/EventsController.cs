using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Entities;
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
    private readonly IVenueRepository _venueRepo;

    public EventsController(IMapper mapper, IEventRepository eventRepo, IVenueRepository venueRepo) : base(mapper)
    {
        _eventRepo = eventRepo;
        _venueRepo = venueRepo;
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
    public async Task<IActionResult> Create([FromBody] EventCreationBindingModel bm)
    {
        var _event = await _eventRepo.FindByNameAndDate(bm.Name, bm.Date);

        if (_event != null)
            return Conflict("An event exists with the same name and date.");

        var venueMap = await _venueRepo.GetAll(bm.Venues.Select(x => x.VenueId));
        var venues = new List<(int, Venue)>();

        foreach (var venuePriority in bm.Venues)
            if (venueMap.ContainsKey(venuePriority.VenueId))
                venues.Add((venuePriority.Priority, venueMap[venuePriority.VenueId]));

        _event = await _eventRepo.Create(bm.Name, bm.Date, venues);
        return Ok(Mapper.Map<EventViewModel>(_event));
    }
}