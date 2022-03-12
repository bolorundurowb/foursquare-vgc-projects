namespace neophyte.api.Models.Binding;

public class PersonCreationBindingModel
{
    /// <summary>
    /// The church attendee's first name
    /// </summary>
    /// <example>Samuel</example>
    public string FirstName { get; set; }

    /// <summary>
    /// The church attendee's last name
    /// </summary>
    /// <example>Olatunji</example>
    public string LastName { get; set; }

    /// <summary>
    /// The church attendee's mobile phone number
    /// </summary>
    /// <example>0801243567</example>
    public string Phone { get; set; }
}