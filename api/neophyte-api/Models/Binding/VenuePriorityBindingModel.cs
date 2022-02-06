using Swashbuckle.AspNetCore.Annotations;

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
    /// <example>61fff8966b22c10a9e790d57</example>
    [SwaggerSchema(Nullable = false)]
    public string VenueId { get; set; }
}