﻿using System;
using System.Collections.Generic;
using api.Data.Enums;
using MongoDB.Bson;

namespace api.Data.ValueObjects;

public class EventSeat : ValueObject
{
    public int Priority { get; private set; }

    public SeatCategory Category { get; private set; }

    public string Number { get; private set; }

    public string AttachedNumber { get; private set; }

    public ObjectId? PersonId { get; private set; }

    private EventSeat()
    {
    }

    public EventSeat(int priority, Seat seat)
    {
        Priority = priority;
        Category = seat.Category;
        Number = seat.Number;
        AttachedNumber = seat.AttachedNumber;
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