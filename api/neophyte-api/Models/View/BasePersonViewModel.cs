using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class BasePersonViewModel
{
    /// <summary>
    /// The church attendee internal identifier
    /// </summary>
    /// <example>61fff8966b22c10a9e790d57</example>
    [SwaggerSchema(Nullable = false)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// The church attendee's first name
    /// </summary>
    /// <example>Jane</example>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// The church attendee's last name
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; } = null!;
}