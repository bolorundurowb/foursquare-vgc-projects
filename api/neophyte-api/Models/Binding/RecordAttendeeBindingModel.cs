using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class RecordAttendeeBindingModel
{
    [SwaggerSchema(Nullable = false)]
    public string FirstName { get; set; } = null!;

    [SwaggerSchema(Nullable = false)]
    public string LastName { get; set; } = null!;

    public string? SeatNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? PhoneNumber { get; set; }
}
