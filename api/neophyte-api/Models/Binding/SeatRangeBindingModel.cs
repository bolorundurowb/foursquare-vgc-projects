using neophyte.api.Data.Enums;

namespace neophyte.api.Models.Binding;

public class SeatRangeBindingModel
{
    /// <summary>
    /// The seat type
    /// </summary>
    /// <example>Couples</example>
    public SeatCategory Category { get; set; }

    /// <summary>
    /// The seat number range or a single seat number
    /// </summary>
    /// <example>A1-A100, D77</example>
    public string NumberRange { get; set; }
}