using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class BaseVenueViewModel
{
    /// <summary>
    /// The venue identifier
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [SwaggerSchema(Nullable = false)]
    public string Id { get; set; }

    /// <summary>
    /// The venue name
    /// </summary>
    /// <example>Teens Church</example>
    [SwaggerSchema(Nullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// The number of seats in the venue
    /// </summary>
    /// <example>101</example>
    public int NumOfSeats { get; set; }
}