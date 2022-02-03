using neophyte.api.Data.Enums;

namespace neophyte.api.Models.Binding;

public class SeatAssignmentBindingModel
{
    public string PersonId { get; set; }

    public SeatCategory Category { get; set; }
}