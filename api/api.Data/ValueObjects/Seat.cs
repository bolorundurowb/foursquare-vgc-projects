using System;
using api.Data.Enums;

namespace api.Data.ValueObjects;

public class Seat : IEquatable<Seat>
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

    public bool Equals(Seat other)
    {
        if (ReferenceEquals(null, other)) 
            return false;
        
        if (ReferenceEquals(this, other))
            return true;
        
        return Category == other.Category && Number == other.Number;
    }

    public override int GetHashCode() => HashCode.Combine((int)Category, Number);

    public static bool operator ==(Seat left, Seat right) => Equals(left, right);

    public static bool operator !=(Seat left, Seat right) => !Equals(left, right);
}