using System;
using System.Collections.Generic;
using System.Linq;

namespace neophyte.api.Data.ValueObjects;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetAtomicValues();

    public ValueObject GetCopy() => (ValueObject)MemberwiseClone();

    public bool Equals(ValueObject other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (other.GetType() != GetType())
            return false;

        using (var thisValues = GetAtomicValues().GetEnumerator())
        using (var otherValues = other.GetAtomicValues().GetEnumerator())
        {
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                    return false;

                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current)) return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }
    }

    public override bool Equals(object obj) => obj is ValueObject other && Equals(other);

    public static bool operator ==(ValueObject left, ValueObject right) => left.Equals(right);

    public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

    public override int GetHashCode() =>
        GetAtomicValues()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
}