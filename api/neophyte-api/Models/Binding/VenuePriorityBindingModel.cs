﻿using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class VenuePriorityBindingModel
{
    /// <summary>
    /// The venue priority. Lower values mean higher priority
    /// </summary>
    /// <example>1</example>
    public int Priority { get; set; }

    /// <summary>
    /// The venue identifier
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [SwaggerSchema(Nullable = false)]
    public string VenueId { get; set; }
}