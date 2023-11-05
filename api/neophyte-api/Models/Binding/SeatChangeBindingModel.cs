using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class SeatChangeBindingModel
{
    /// <summary>
    /// The church attendee identifier
    /// </summary>
    /// <example>61fff8966b22c10a9e790d57</example>
    [SwaggerSchema(Nullable = false)]
    public string PersonId { get; set; } = null!;

    /// <summary>
    /// The venue which the seat number should be assigned in
    /// </summary>
    /// <example>C5</example>
    /// <example>61fff8966b22c10a9e790d57</example>
    [SwaggerSchema(Nullable = false)]
    public string VenueId { get; set; } = null!;

    /// <summary>
    /// The seat number
    /// </summary>
    /// <example>C5</example>
    [SwaggerSchema(Nullable = false)]
    public string SeatNumber { get; set; } = null!;
}