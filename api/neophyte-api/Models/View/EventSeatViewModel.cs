using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class EventSeatViewModel : SeatViewModel
{
    /// <summary>
    /// The seat priority (obtained from the venue it is in). Lower values mean higher priority
    /// </summary>
    /// <example>2</example>
    public int Priority { get; private set; }


    /// <summary>
    /// The venue internal identifier
    /// </summary>
    /// <example>61fff8966b22c10a9e790d57</example>
    [SwaggerSchema(Nullable = false)]
    public string VenueId { get; private set; }

    /// <summary>
    /// The name of the venue that the seat is in
    /// </summary>
    /// <example>Main Auditorium</example>
    [SwaggerSchema(Nullable = false)]
    public string VenueName { get; private set; }
}