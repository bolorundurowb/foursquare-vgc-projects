using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class EventCreationBindingModel
{
    /// <summary>
    /// The event name
    /// </summary>
    /// <example>Sunday Service</example>
    [SwaggerSchema(Nullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// The event date and time. Must be in the future
    /// </summary>
    /// <example>2022-02-04T00:00:00.000Z</example>
    public DateTime Date { get; set; }

    /// <summary>
    /// The event duration in minutes
    /// </summary>
    /// <example>30</example>
    public int DurationInMinutes { get; set; }

    /// <summary>
    /// The list of venues to be used in order of priority
    /// </summary>
    [SwaggerSchema(Nullable = false)]
    public List<VenuePriorityBindingModel> Venues { get; set; }
}