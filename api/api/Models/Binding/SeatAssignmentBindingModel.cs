using api.Data.Enums;

namespace api.Models.Binding;

public class SeatAssignmentBindingModel
{
    public string PersonId { get; set; }

    public SeatCategory Category { get; set; }
}