using System;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class BaseEventViewModel
{
    /// <summary>
    /// The event internal identifier
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [SwaggerSchema(Nullable = false)]
    public string Id { get; set; }

    /// <summary>
    /// The event name
    /// </summary>
    /// <example>Sunday Service</example>
    [SwaggerSchema(Nullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// The event date and time
    /// </summary>
    /// <example>2022-02-04T00:00:00.000Z</example>
    public DateTime Date { get; set; }

    /// <summary>
    /// The number of attendees for the event. It would be 0 for future events
    /// </summary>
    /// <example>77</example>
    public int NumOfAttendees { get; set; }
}