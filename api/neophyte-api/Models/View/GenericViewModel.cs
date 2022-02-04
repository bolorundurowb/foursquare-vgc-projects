using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class GenericViewModel
{
    /// <summary>
    /// The API response message
    /// </summary>
    /// <example>A response message</example>
    [SwaggerSchema(Nullable = false)]
    public string Message { get; set; }
}