using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class SeatChangeBindingModel
{
    /// <summary>
    /// The church attendee identifier
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    public string PersonId { get; set; }

    /// <summary>
    /// The seat number
    /// </summary>
    /// <example>C5</example>
    [SwaggerSchema(Nullable = false)]
    public string SeatNumber { get; set; }
}