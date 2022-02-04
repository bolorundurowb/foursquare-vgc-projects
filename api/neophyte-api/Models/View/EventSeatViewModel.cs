using neophyte.api.Data.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class EventSeatViewModel
{
    /// <summary>
    /// The seat priority (obtained from the venue it is in). Lower values mean higher priority
    /// </summary>
    /// <example>2</example>
    public int Priority { get; private set; }

    /// <summary>
    /// The seat type
    /// </summary>
    /// <example>Couples</example>
    public SeatCategory Category { get; private set; }

    /// <summary>
    /// The name of the venue that the seat is in
    /// </summary>
    /// <example>Main Auditorium</example>
    [SwaggerSchema(Nullable = false)]
    public string VenueName { get; private set; }

    /// <summary>
    /// The seat number
    /// </summary>
    /// <example>C5</example>
    [SwaggerSchema(Nullable = false)]
    public string Number { get; private set; }

    /// <summary>
    /// The couple seat number (if it is a couples seat)
    /// </summary>
    /// <example>C5A</example>
    public string AssociatedNumber { get; private set; }
}