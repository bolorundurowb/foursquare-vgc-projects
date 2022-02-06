using System;
using System.Collections.Generic;
using MongoDB.Bson;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Enums;

namespace neophyte.api.Data.ValueObjects;

public class EventSeat : ValueObject
{
    public int Priority { get; private set; }

    public SeatCategory Category { get; private set; }

    public ObjectId VenueId { get; private set; }

    public string VenueName { get; private set; }

    public string Number { get; private set; }

    public string AssociatedNumber { get; private set; }

    public ObjectId? PersonId { get; private set; }

    private EventSeat()
    {
    }

    public EventSeat(int priority, Venue venue, Seat seat)
    {
        Priority = priority;
        VenueId = (ObjectId)venue.Id;
        VenueName = venue.Name;
        Number = seat.Number;
        Category = seat.Category;
        AssociatedNumber = seat.AssociatedNumber;
    }

    public void Assign(ObjectId personId)
    {
        if (PersonId != null && PersonId != ObjectId.Empty)
            throw new InvalidOperationException("This seat has been assigned.");

        PersonId = personId;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Priority;
        yield return Category;
        yield return Number;
        yield return PersonId;
    }
}