using api.Data.Enums;

namespace neophyte;

public class SeatAssignmentBindingModel
{
    public string PersonId { get; set; }

    public SeatCategory Category { get; set; }
}