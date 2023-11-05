using neophyte.api.Core.Extensions;

namespace neophyte.api.Data.Entities;

public class Attendee
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string? EmailAddress { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? SeatNumber { get; private set; }

    private Attendee() { }

    public Attendee(string firstName, string lastName, string? emailAddress, string? phoneNumber, string? seatNumber)
    {
        FirstName = firstName.Trim().ToTitleCase();
        LastName = lastName.Trim().ToTitleCase();
        PhoneNumber = phoneNumber?.Trim();
        EmailAddress = emailAddress?.Trim().ToLowerInvariant();
        SeatNumber = seatNumber?.Trim().ToUpperInvariant();
    }
}
