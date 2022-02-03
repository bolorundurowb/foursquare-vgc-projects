using neophyte.api.Data.Enums;

namespace neophyte.api.Models.View;

public class EventAttendeeViewModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Venue { get; set; }

    public SeatCategory Category { get; set; }

    public string Seat { get; set; }

    public string AccompanyingSeat { get; set; }
}