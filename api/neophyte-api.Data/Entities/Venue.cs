using System;
using System.Collections.Generic;
using System.Linq;
using meerkat;
using meerkat.Attributes;
using neophyte.api.Data.Enums;
using neophyte.api.Data.ValueObjects;

namespace neophyte.api.Data.Entities;

[Collection(TrackTimestamps = true)]
public class Venue : Schema
{
    public string Name { get; private set; }

    public List<Seat> Seats { get; private set; }

    private Venue()
    {
    }

    public Venue(string name) => Name = name;

    public void AddSeat(SeatCategory category, string seatNumber)
    {
        if (string.IsNullOrWhiteSpace(seatNumber))
            throw new ArgumentException("Invalid seatNumber number.", nameof(seatNumber));

        seatNumber = seatNumber.ToUpperInvariant();
        Seats ??= new List<Seat>();

        if (Seats.Any(x => x.Category == category && x.Number == seatNumber))
            return;

        Seats.Add(new Seat(category, seatNumber));
    }

    public void RemoveSeat(string seatNumber)
    {
        if (string.IsNullOrWhiteSpace(seatNumber))
            throw new ArgumentException("Invalid seat number.", nameof(seatNumber));

        seatNumber = seatNumber.ToUpperInvariant();
        Seats ??= new List<Seat>();
        var seat = Seats.FirstOrDefault(x => x.Number == seatNumber);

        if (seat == null)
            return;

        Seats.Remove(seat);
    }
}