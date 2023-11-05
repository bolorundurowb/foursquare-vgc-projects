using neophyte.api.Data.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class SeatViewModel
{
    /// <summary>
    /// The seat type
    /// </summary>
    /// <example>Couples</example>
    public SeatCategory Category { get; set; }

    /// <summary>
    /// The seat number
    /// </summary>
    /// <example>C5</example>
    [SwaggerSchema(Nullable = false)]
    public string Number { get; set; } = null!;

    /// <summary>
    /// The couple seat number (if it is a couples seat)
    /// </summary>
    /// <example>C5A</example>
    public string AssociatedNumber { get; set; } = null!;
}