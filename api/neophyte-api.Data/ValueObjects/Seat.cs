using System.Collections.Generic;
using neophyte.api.Data.Enums;

namespace neophyte.api.Data.ValueObjects;

public class Seat : ValueObject
{
    public SeatCategory Category { get; private set; }

    public string Number { get; private set; }

    public string AssociatedNumber { get; private set; }

    private Seat()
    {
    }

    public Seat(SeatCategory category, string number)
    {
        Category = category;
        Number = number;
        AssociatedNumber = category == SeatCategory.Couples ? number + "A" : null;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Category;
        yield return Number;
        yield return AssociatedNumber;
    }
}