using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class EventViewModel : BaseEventViewModel
{
    /// <summary>
    /// The event registration url
    /// </summary>
    /// <example>https://example.com/events/61fff8966b22c10a9e790d57</example>
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