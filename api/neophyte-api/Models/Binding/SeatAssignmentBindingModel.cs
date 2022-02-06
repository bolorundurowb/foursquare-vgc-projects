using neophyte.api.Data.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class SeatAssignmentBindingModel
{
    /// <summary>
    /// The church attendee identifier
    /// </summary>
    /// <example>61fff8966b22c10a9e790d57</example>
    [SwaggerSchema(Nullable = false)]
    public string PersonId { get; set; }

    /// <summary>
    /// The seat type
    /// </summary>
    /// <example>Couples</example>
    public SeatCategory Category { get; set; }
}