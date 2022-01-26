using System;
using System.Collections.Generic;
using System.Linq;
using api.Data.Enums;
using api.Data.ValueObjects;
using dotenv.net.Utilities;
using meerkat;
using meerkat.Attributes;
using MongoDB.Bson;

namespace api.Data.Entities;

[Collection(TrackTimestamps = true)]
public class Event : Schema
{
    public string Name { get; private set; }

    public DateTime Date { get; private set; }

    public string RegistrationUrl { get; private set; }

    public List<EventSeat> AvailableSeats { get; private set; }

    public List<EventSeat> AssignedSeats { get; private set; } = new();

    public Event(string name, DateTime eventDate, List<(int Priority, Venue Venue)> venuePriority)
    {
        Name = name;
        Date = eventDate;
        AvailableSeats = new List<EventSeat>();

        foreach (var (priority, venue) in venuePriority)
        foreach (var seat in venue.Seats)
            AvailableSeats.Add(new EventSeat(priority, seat));

        // generate url and QR code
        EnvReader.TryGetStringValue("UI_URL", out var baseUrl);
        RegistrationUrl = $"{baseUrl}/events/{Id}";
    }

    public EventSeat SelectSeat(SeatCategory category, ObjectId personId)
    {
        var seat = AssignedSeats.FirstOrDefault(x => x.PersonId == personId);

        if (seat != null)
            throw new Exception("A seat has been assigned to this person for this event.");

        seat = AvailableSeats.OrderBy(x => x.Priority)
            .ThenBy(x => x.Number)
            .FirstOrDefault(x => x.Category == category);

        if (seat == null)
            throw new Exception("No seat of the category available.");

        AvailableSeats.Remove(seat);
        seat.Assign(personId);
        AssignedSeats.Add(seat);

        return seat;
    }
}