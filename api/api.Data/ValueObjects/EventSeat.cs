using System;
using System.Collections.Generic;
using api.Data.Enums;
using MongoDB.Bson;

namespace api.Data.ValueObjects;

public class EventSeat : ValueObject
{
    public int Order { get; private set; }

    public SeatCategory Category { get; private set; }

    public string Number { get; private set; }

    public ObjectId? PersonId { get; private set; }

    private EventSeat()
    {
    }

    public EventSeat(int order, Seat seat)
    {
        Order = order;
        Category = seat.Category;
        Number = seat.Number;
    }

    public void Assign(ObjectId personId)
    {
        if (PersonId != null && PersonId != ObjectId.Empty)
            throw new InvalidOperationException("This seat has been assigned.");

        PersonId = personId;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Order;
        yield return Category;
        yield return Number;
        yield return PersonId;
    }
}