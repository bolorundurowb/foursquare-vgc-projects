using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class EventViewModel : BaseEventViewModel
{
    /// <summary>
    /// The event registration url
    /// </summary>
    /// <example>https://example.com/events/123e4567-e89b-12d3-a456-426614174000</example>
    [SwaggerSchema(Nullable = false)]
    public string RegistrationUrl { get; set; }

    /// <summary>
    /// The event registration url encoded as a QR code PNG in base64
    /// </summary>
    /// <example>782hjwe87432yu2784iun3780hyd4yur879348374</example>
    [SwaggerSchema(Nullable = false)]
    public string RegistrationUrlQrCode { get; set; }

    /// <summary>
    /// The list of seats available for this event in order of priority
    /// </summary>
    public List<EventSeatViewModel> AvailableSeats { get; set; }
}