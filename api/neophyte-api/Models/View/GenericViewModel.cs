using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class GenericViewModel
{
    /// <summary>
    /// The error message
    /// </summary>
    /// <example>An error occurred</example>
    [SwaggerSchema(Nullable = false)]
    public string Message { get; set; }
}