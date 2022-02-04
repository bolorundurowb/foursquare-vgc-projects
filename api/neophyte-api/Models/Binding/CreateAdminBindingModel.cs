using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class CreateAdminBindingModel
{
    [SwaggerSchema(Nullable = false)] 
    public string Name { get; set; }

    [SwaggerSchema(Nullable = false)] 
    public string EmailAddress { get; set; }

    [SwaggerSchema(Nullable = false)] 
    public string Password { get; set; }
}