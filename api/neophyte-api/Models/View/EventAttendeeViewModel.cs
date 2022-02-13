using neophyte.api.Data.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class EventAttendeeViewModel
{
    /// <summary>
    /// The church attendee's first name
    /// </summary>
    /// <example>Jane</example>
    public string FirstName { get; set; }

    /// <summary>
    /// The church attendee's last name
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; }

    /// <summary>
    /// The church attendee's mobile phone number
    /// </summary>
    /// <example>0801243567</example>
    [SwaggerSchema(Nullable = false)]
    public string Phone { get; set; }

    /// <summary>
    /// The name of the venue that the attendee sat
    /// </summary>
    /// <example>Ground Floor</example>
    [SwaggerSchema(Nullable = false)]
    public string Venue { get; set; }

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
    public string Seat { get; set; }

    /// <summary>
    /// The couple seat number (if it is a couples seat)
    /// </summary>
    /// <example>C5A</example>
    public string AccompanyingSeat { get; set; }
}