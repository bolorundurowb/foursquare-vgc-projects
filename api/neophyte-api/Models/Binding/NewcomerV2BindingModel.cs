using neophyte.api.Data.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.Binding;

public class NewcomerV2BindingModel
{
    /// <summary>
    /// The full name of the newcomer
    /// </summary>
    /// <example>John Doe</example>
    [SwaggerSchema(Nullable = false)]
    public string FullName { get; set; }

    /// <summary>
    /// The newcomers house address
    /// </summary>
    /// <example>12, Babatunde Johnson, Ajah</example>
    public string HomeAddress { get; set; }

    /// <summary>
    /// The newcomers mobile phone number
    /// </summary>
    /// <example>0801243567</example>
    public string Phone { get; set; }

    /// <summary>
    /// The newcomers email address
    /// </summary>
    /// <example>john@doe.com</example>
    public string EmailAddress { get; set; }

    /// <summary>
    /// The newcomers birth day and month
    /// </summary>
    /// <example>June 7</example>
    public string BirthDay { get; set; }

    /// <summary>
    /// The newcomers gender if provided
    /// </summary>
    /// <example>Female</example>
    public Gender? Gender { get; set; }

    /// <summary>
    /// The newcomers age group range
    /// </summary>
    /// <example>41 - 45</example>
    public string AgeGroup { get; set; }

    /// <summary>
    /// The newcomers additional comments or prayer points
    /// </summary>
    /// <example>Lorem ipsum dolor emet</example>
    public string CommentsOrPrayers { get; set; }

    /// <summary>
    /// How the newcomer came across the church
    /// </summary>
    /// <example>Facebook</example>
    public string HowYouFoundUs { get; set; }

    /// <summary>
    /// Whether the newcomer is born againb
    /// </summary>
    /// <example>No</example>
    public MultiChoice? BornAgain { get; set; }

    /// <summary>
    /// The newcomers choice about becoming a church member
    /// </summary>
    /// <example>Maybe</example>
    public MultiChoice? BecomeMember { get; set; }
}