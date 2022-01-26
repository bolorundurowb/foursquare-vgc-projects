using System;
using System.Collections.Generic;
using api.Data.Enums;
using MongoDB.Bson;

namespace api.Data.ValueObjects;

public class EventSeat : ValueObject
{
    public int Priority { get; private set; }

    public SeatCategory Category { get; private set; }

    public string VenueName { get; private set; }

    public string Number { get; private set; }

    public string AssociatedNumber { get; private set; }

    public ObjectId? PersonId { get; private set; }

    private EventSeat()
    {
    }

    public EventSeat(int priority, string venueName, Seat seat)
    {
        Priority = priority;
        VenueName = venueName;
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