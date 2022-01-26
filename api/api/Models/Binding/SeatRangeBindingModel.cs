using api.Data.Enums;

namespace api.Models.Binding;

public class SeatRangeBindingModel
{
    public SeatCategory Category { get; set; }
    
    public string Number { get; set; }
}