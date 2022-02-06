using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;

namespace neophyte.api.Controllers.v1;

[ApiVersion("1.0")]
public class EventsController : ApiController
{
    private readonly IEventRepository _eventRepo;
    private readonly IVenueRepository _venueRepo;
    private readonly IPersonsRepository _personsRepo;

    public EventsController(IMapper mapper, IEventRepository eventRepo, IVenueRepository venueRepo,
        IPersonsRepository personsRepo) : base(mapper)
    {
        _eventRepo = eventRepo;
        _venueRepo = venueRepo;
        _personsRepo = personsRepo;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<BaseEventViewModel>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] CollectionQueryModel qm)
    {
        var events = await _eventRepo.GetAll(qm.Skip.Value, qm.Limit.Value);
        return Ok(Mapper.Map<List<BaseEventViewModel>>(events));
    }

    [HttpGet("{eventId}")]
    [ProducesResponseType(typeof(EventViewModel), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    public async Task<IActionResult> GetOne(string eventId)
    {
        var @event = await _eventRepo.FindById(eventId);

        if (@event == null)
            return NotFound("Event not found.");

        return Ok(Mapper.Map<EventViewModel>(@event));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(EventViewModel), 201)]
    [ProducesResponseType(typeof(GenericViewModel), 409)]
    public async Task<IActionResult> Create([FromBody] EventCreationBindingModel bm)
    {
        var @event = await _eventRepo.FindByNameAndDate(bm.Name, bm.Date);

        if (@event != null)
            return Conflict("An event exists with the same name and date.");

        var venueMap = await _venueRepo.FindAndMapById(bm.Venues.Select(x => x.VenueId));
        var venues = new List<(int, Venue)>();

        foreach (var venuePriority in bm.Venues)
            if (venueMap.ContainsKey(venuePriority.VenueId))
                venues.Add((venuePriority.Priority, venueMap[venuePriority.VenueId]));

        @event = await _eventRepo.Create(bm.Name, bm.Date, venues);
        return Ok(Mapper.Map<EventViewModel>(@event));
    }

    [AllowAnonymous]
    [HttpPost("{eventId}/checkin")]
    [ProducesResponseType(typeof(EventViewModel), 201)]
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    [ProducesResponseType(typeof(GenericViewModel), 409)]
    public async Task<IActionResult> CheckIn(string eventId, [FromBody] SeatAssignmentBindingModel bm)
    {
        var @event = await _eventRepo.FindById(eventId);

        if (@event == null)
            return NotFound("Event not found.");

        if (@event.HasSeatAssigned(bm.PersonId))
            return Conflict("A seat has been assigned.");

        var person = await _personsRepo.FindById(bm.PersonId);

        if (person == null)
            return NotFound("Person not found.");

        var eventSeat = await _eventRepo.AssignSeat(@event, bm.Category, person);
        return Ok(Mapper.Map<EventSeatViewModel>(eventSeat));
    }

    [HttpPost("{eventId}/change-seats")]
    [ProducesResponseType(typeof(EventViewModel), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    [ProducesResponseType(typeof(GenericViewModel), 409)]
    public async Task<IActionResult> ChangeSeat(string eventId, [FromBody] SeatChangeBindingModel bm)
    {
        var @event = await _eventRepo.FindById(eventId);
        var venueId = ObjectId.Parse(bm.VenueId);

        if (@event == null)
            return NotFound("Event not found.");

        if (!@event.HasSeatAssigned(bm.PersonId))
            return BadRequest("A seat has not been assigned for this person. Register them afresh.");

        if (@event.IsSeatAvailable(venueId, bm.SeatNumber))
            return BadRequest("The seat selected has already been assigned.");

        var person = await _personsRepo.FindById(bm.PersonId);

        if (person == null)
            return NotFound("Person not found.");

        var eventSeat = await _eventRepo.ChangeSeat(@event, person, venueId, bm.SeatNumber);
        return Ok(Mapper.Map<EventSeatViewModel>(eventSeat));
    }

    /// <summary>
    /// Returns a list of attendees for the given Event
    /// </summary>
    /// <param name="eventId">The id for the event</param>
    [HttpGet("{eventId}/attendees")]
    [ProducesResponseType(typeof(List<EventAttendeeViewModel>), 200)]
    public async Task<IActionResult> GetAttendees(string eventId)
    {
        var @event = await _eventRepo.FindById(eventId);

        if (@event == null)
            return NotFound("Event not found.");

        var attendees = await _eventRepo.GetAttendees(@event);
        return Ok(Mapper.Map<List<EventAttendeeViewModel>>(attendees));
    }
}