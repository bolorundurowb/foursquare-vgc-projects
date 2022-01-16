using System;
using System.Collections.Generic;
using meerkat;
using meerkat.Attributes;

namespace api.Data.Entities;

[Collection(TrackTimestamps = true)]
public class Venue : Schema
{
    public string Name { get; private set; }

    public List<string> Seats { get; private set; }

    private Venue()
    {
    }

    public Venue(string name) => Name = name;

    public void UpdateName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name.Trim();
    }

    public void AddSeat(string seat)
    {
        if (string.IsNullOrWhiteSpace(seat))
            throw new ArgumentException("Invalid seat name.", nameof(seat));

        seat = seat.ToUpperInvariant();
        Seats ??= new List<string>();

        if (Seats.Contains(seat))
            return;

        Seats.Add(seat);
    }

    public void RemoveSeat(string seat)
    {
        if (string.IsNullOrWhiteSpace(seat))
            throw new ArgumentException("Invalid seat name.", nameof(seat));

        seat = seat.ToUpperInvariant();
        Seats ??= new List<string>();

        if (!Seats.Contains(seat))
            return;

        Seats.Remove(seat);
    }
}