using meerkat;
using meerkat.Attributes;
using neophyte.api.Shared.Extensions;

namespace neophyte.api.Data.Entities;

[Collection(TrackTimestamps = true)]
public class Person : Schema
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Phone { get; private set; }

    private Person()
    {
    }

    public Person(string firstName, string lastName, string phone)
    {
        FirstName = firstName?.Trim();
        LastName = lastName?.Trim();
        Phone = phone?.Regularize().Trim();
    }
}