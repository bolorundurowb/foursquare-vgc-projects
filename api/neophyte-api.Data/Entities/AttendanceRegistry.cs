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

    public AttendanceRegistry(DateOnly date)
    {
        Date = date;
        Attendees = new List<Attendee>();
    }

    public Attendee AddAttendee(string firstName, string lastName, string? emailAddress, string? phoneNumber, string? seatNumber)
    {
        var attendee = new Attendee(firstName, lastName, emailAddress, phoneNumber, seatNumber);
        Attendees.Add(attendee);

        return attendee;
    }
}
