using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class CreateAdminBindingModel
{
    /// <summary>
    /// The full name of the admin
    /// </summary>
    /// <example>John Doe</example>
    [SwaggerSchema(Nullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// The admins email address
    /// </summary>
    /// <example>john@doe.com</example>
    [SwaggerSchema(Nullable = false)]
    public string EmailAddress { get; set; }

    /// <summary>
    /// The default admin of the password
    /// </summary>
    /// <example>Y0urP@$5word</example>
    [SwaggerSchema(Nullable = false)]
    public string Password { get; set; }
}