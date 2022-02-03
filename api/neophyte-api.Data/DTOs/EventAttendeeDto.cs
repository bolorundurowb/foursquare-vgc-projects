using neophyte.api.Data.Enums;

namespace neophyte.api.Data.DTOs;

public class EventAttendeeDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Venue { get; set; }

    public SeatCategory Category { get; set; }

    public string Seat { get; set; }

    public string AccompanyingSeat { get; set; }
}