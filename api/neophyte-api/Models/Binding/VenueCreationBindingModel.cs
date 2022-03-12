using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class VenueCreationBindingModel
{
    /// <summary>
    /// The venue name
    /// </summary>
    /// <example>Teens Church</example>
    [SwaggerSchema(Nullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// Seat ranges that are in this venue
    /// </summary>
    [SwaggerSchema(Nullable = false)]
    public List<SeatRangeBindingModel> SeatRanges { get; set; }
}