using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class BasePersonViewModel
{
    /// <summary>
    /// The church attendee internal identifier
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [SwaggerSchema(Nullable = false)]
    public string Id { get; set; }

    /// <summary>
    /// The church attendee's first name
    /// </summary>
    /// <example>Jane</example>
    public string FirstName { get; set; }

    /// <summary>
    /// The church attendee's last name
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; }
}