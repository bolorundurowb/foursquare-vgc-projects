using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class UpdatePasswordBindingModel
{
    /// <summary>
    /// The new password
    /// </summary>
    /// <example>Y0urP@$5word</example>
    [SwaggerSchema(Nullable = false)]
    public string Password { get; set; }

    /// <summary>
    /// A confirmation of the new password. It must match the 'password' field
    /// </summary>
    /// <example>Y0urP@$5word</example>
    [SwaggerSchema(Nullable = false)]
    public string ConfirmPassword { get; set; }
}