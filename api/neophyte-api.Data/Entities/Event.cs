using System;
using System.Collections.Generic;
using System.Linq;
using dotenv.net.Utilities;
using meerkat;
using meerkat.Attributes;
using MongoDB.Bson;
using neophyte.api.Data.Enums;
using neophyte.api.Data.ValueObjects;

namespace neophyte.api.Data.Entities;

[Collection(TrackTimestamps = true)]
public class Event : Schema
{
    public string Name { get; private set; }

    public DateTime Date { get; private set; }

    public string RegistrationUrl { get; private set; }

    public List<EventSeat> AvailableSeats { get; private set; }

    public List<EventSeat> AssignedSeats { get; private set; } = new();

    private Event()
    {
    }

    public Event(string name, DateTime eventDate, List<(int Priority, Venue Venue)> venuePriority)
    {
        Name = name;
        Date = eventDate;
        AvailableSeats = new List<EventSeat>();

        foreach (var (priority, venue) in venuePriority)
        foreach (var seat in venue.Seats)
            AvailableSeats.Add(new EventSeat(priority, venue, seat));

        // generate url and QR code
        EnvReader.TryGetStringValue("UI_URL", out var baseUrl);
        RegistrationUrl = $"{baseUrl}/events/{Id}";
    }

    public EventSeat SelectSeat(SeatCategory category, Person person)
    {
        var personId = (ObjectId)person.Id;
        var eventSeat = AssignedSeats.FirstOrDefault(x => x.PersonId == personId);

        if (eventSeat != null)
            return eventSeat;

        eventSeat = AvailableSeats.OrderBy(x => x.Priority)
            .ThenBy(x => x.Number)
            .FirstOrDefault(x => x.Category == category);

        if (eventSeat == null)
            throw new Exception("No seat of the category available.");

        AvailableSeats.Remove(eventSeat);
        eventSeat.Assign(personId);
        AssignedSeats.Add(eventSeat);

        return eventSeat;
    }

    public EventSeat ChangeSeat(Person person, string seatNumber)
    {
        var personId = (ObjectId)person.Id;
        seatNumber = seatNumber?.ToUpperInvariant();
        var currentSeat = AssignedSeats.FirstOrDefault(x => x.PersonId == personId);

        if (currentSeat == null)
            throw new Exception("There is no assigned seat for this person. So it cant be changed.");

        // put back into the pool
        AvailableSeats.Insert(0, (EventSeat)currentSeat.GetCopy());

        // assign the new seats
        var eventSeat = AvailableSeats.First(x => x.Number == seatNumber);
        AvailableSeats.Remove(eventSeat);

        eventSeat.Assign(personId);
        AssignedSeats.Add(eventSeat);

        return eventSeat;
    }

    public bool HasSeatAssigned(string personId) => AssignedSeats.Any(x => x.PersonId == ObjectId.Parse(personId));

    public bool IsSeatAvailable(ObjectId venueId, string seatNumber)
    {
        seatNumber = seatNumber?.ToUpperInvariant();
        return AvailableSeats.Any(x => x.VenueId == venueId && x.Number == seatNumber);
    }
}