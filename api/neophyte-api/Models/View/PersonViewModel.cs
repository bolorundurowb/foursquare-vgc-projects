using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class PersonViewModel : BasePersonViewModel
{
    /// <summary>
    /// The church attendee's full name
    /// </summary>
    /// <example>Jane Doe</example>
    [SwaggerSchema(Nullable = false)]
    public string FullName { get; set; } = null!;

    /// <summary>
    /// The church attendee's mobile phone number
    /// </summary>
    /// <example>0801243567</example>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// The church attendee's identifier encoded as a QR code PNG in base64
    /// </summary>
    /// <example>782hjwe87432yu2784iun3780hyd4yur879348374</example>
    [SwaggerSchema(Nullable = false)]
    public string QrUrl { get; set; } = null!;
}