using System;
using meerkat;
using meerkat.Attributes;
using moment.net;
using neophyte.api.Data.Enums;

namespace neophyte.api.Data.Entities;

[Collection(Name = "attendees", TrackTimestamps = true)]
public class Attendee : Schema
{
    public DateOnly Date { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string? EmailAddress { get; private set; }

    public string? PhoneNumber { get; private set; }

    private Attendee()
    {
    }

    public Attendee(string firstName, string lastName, string phone, string seatAssigned, string seatType)
    {
        Date = DateTime.UtcNow.Date;
        FullName = $"{firstName} {lastName}";
        Phone = phone;
        SeatAssigned = seatAssigned;
        SeatType = seatType;
    }

    public Attendee(string emailAddress, string fullName, int? age, string phone, string residentialAddress,
        Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers, bool caredForSickPerson,
        MultiChoice? haveCovidSymptoms)
    {
        // they should be registering against the next sunday
        Date = DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday
            ? DateTime.UtcNow.Date
            : DateTime.UtcNow.Date.Next(DayOfWeek.Sunday);
        EmailAddress = emailAddress;
        FullName = fullName;
        Age = age;
        Phone = phone;
        ResidentialAddress = residentialAddress;
        Gender = gender;
        ReturnedInLastTenDays = returnedInLastTenDays;
        LiveWithCovidCaregivers = liveWithCovidCaregivers;
        CaredForSickPerson = caredForSickPerson;
        HaveCovidSymptoms = haveCovidSymptoms;
    }
}