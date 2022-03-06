using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meerkat;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using neophyte.api.Data.DTOs;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Enums;
using neophyte.api.Data.Generators;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Data.ValueObjects;

namespace neophyte.api.Data.Repositories.Implementations;

public class EventRepository : IEventRepository
{
    public Task<List<Event>> GetAll(int skip, int limit) =>
        Meerkat.Query<Event>()
            .OrderByDescending(x => x.StartsAt)
            .Skip(skip)
            .Take(limit)
            .ToListAsync();

    public Task<Event> FindById(string eventId) => Meerkat.FindByIdAsync<Event>(ObjectId.Parse(eventId));

    public Task<Event> FindByNameAndDate(string name, DateTime date) =>
        Meerkat.FindOneAsync<Event>(x => x.Name == name && x.StartsAt == date);

    public async Task<Event> Create(string name, DateTime date, int durationInMinutes,
        List<(int Priority, Venue Venue)> venuePriority)
    {
        var @event = new Event(name, date, durationInMinutes, venuePriority);
        await @event.SaveAsync();

        return @event;
    }

    public async Task<EventSeat> AssignSeat(Event @event, SeatCategory category, Person person)
    {
        var eventSeat = @event.SelectSeat(category, person);
        await @event.SaveAsync();

        return eventSeat;
    }

    public async Task<EventSeat> ChangeSeat(Event @event, Person person, ObjectId venueId, string seatNumber)
    {
        var eventSeat = @event.ChangeSeat(person, venueId, seatNumber);
        await @event.SaveAsync();

        return eventSeat;
    }

    public async Task<List<EventAttendeeDto>> GetAttendees(Event @event)
    {
        var personIds = @event.AssignedSeats
            .Where(x => x.PersonId != null)
            .Select(x => (object)x.PersonId)
            .Distinct()
            .ToList();
        var persons = await Meerkat.Query<Person>()
            .Where(x => personIds.Contains(x.Id))
            .ToListAsync();
        var personMap = new Dictionary<ObjectId, Person>();

        foreach (var personId in personIds)
        {
            var person = persons.FirstOrDefault(x => personId.Equals(x.Id));
            personMap[(ObjectId)personId] = person;
        }

        var response = new List<EventAttendeeDto>();

        foreach (var eventSeat in @event.AssignedSeats)
        {
            var attendee = new EventAttendeeDto();
            attendee.Venue = eventSeat.VenueName;
            attendee.Category = eventSeat.Category;
            attendee.Seat = eventSeat.Number;
            attendee.AccompanyingSeat = eventSeat.AssociatedNumber;

            if (eventSeat.PersonId.HasValue)
            {
                var personId = eventSeat.PersonId.Value;

                if (personMap.ContainsKey(personId))
                {
                    var person = personMap[personId];

                    if (person != null)
                    {
                        attendee.FirstName = person.FirstName;
                        attendee.LastName = person.LastName;
                        attendee.Phone = person.Phone;
                    }
                }
            }

            response.Add(attendee);
        }

        return response;
    }

    public async Task<(string, byte[])> GetAttendeeReport(Event @event)
    {
        var attendees = await GetAttendees(@event);
        var fileName = $"{@event.Name}_{@event.StartsAt:yyyy-MMM-dd}_attendees.xlsx";
        var data = ExcelGenerator.ForEventAttendees(attendees);
        return (fileName, data);
    }

    public Task Remove(string eventId) => Meerkat.RemoveByIdAsync<Event>(ObjectId.Parse(eventId));

    public async Task OnlineRegister(Event @event, Person person)
    {
        @event.RecordOnlineAttendance(person);
        await @event.SaveAsync();
    }
}