using System;
using System.Collections.Generic;
using meerkat;
using meerkat.Attributes;

namespace neophyte.api.Data.Entities;

[Collection(Name = "attendance-registry", TrackTimestamps = true)]
public class AttendanceRegistry : Schema
{
    public DateOnly Date { get; private set; }

    public List<Attendee> Attendees { get; private set; }

    public AttendanceRegistry()
    {
        Date = DateOnly.FromDateTime(DateTime.UtcNow);
        Attendees = new List<Attendee>();
    }
}
