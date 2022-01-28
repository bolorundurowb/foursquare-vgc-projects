using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Enums;
using api.Data.Repositories.Interfaces;
using api.Data.ValueObjects;
using meerkat;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations;

public class EventRepository : IEventRepository
{
    public Task<List<Event>> GetAll(int skip, int limit) =>
        Meerkat.Query<Event>()
            .OrderByDescending(x => x.Date)
            .Skip(skip)
            .Take(limit)
            .ToListAsync();

    public Task<Event> FindById(string eventId) => Meerkat.FindByIdAsync<Event>(eventId);

    public Task<Event> FindByNameAndDate(string name, DateTime date) =>
        Meerkat.FindOneAsync<Event>(x => x.Name == name && x.Date == date);

    public EventSeat FindSeat(Event @event, string personId) =>
        @event.AssignedSeats.FirstOrDefault(x => x.PersonId == ObjectId.Parse(personId));

    public async Task<Event> Create(string name, DateTime date, List<(int Priority, Venue Venue)> venuePriority)
    {
        var _event = new Event(name, date, venuePriority);
        await _event.SaveAsync();

        return _event;
    }

    public async Task<EventSeat> AssignSeat(Event @event, SeatCategory category, Person person)
    {
        var eventSeat = @event.SelectSeat(category, person);
        await @event.SaveAsync();

        return eventSeat;
    }
}