using System.Collections.Generic;
using api.Data.Enums;

namespace api.Data.ValueObjects;

public class Seat : ValueObject
{
    public SeatCategory Category { get; private set; }

    public string Number { get; private set; }

    private Seat()
    {
    }

    public Seat(SeatCategory category, string number)
    {
        Category = category;
        Number = number;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Category;
        yield return Number;
    }
}