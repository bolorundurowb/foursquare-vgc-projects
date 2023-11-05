using System;
using neophyte.api.Data.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class NewcomerViewModel
{
    /// <summary>
    /// The newcomers internal identifier
    /// </summary>
    /// <example>61fff8966b22c10a9e790d57</example>
    [SwaggerSchema(Nullable = false)]
    public string Id { get; set; }

    /// <summary>
    /// The date this newcomers record was created
    /// </summary>
    /// <example>2022-02-04T00:00:00.000Z</example>
    public DateTime Date { get; set; }

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

    /// <summary>
    /// Any additional remarks the counselor had about the newcomer
    /// </summary>
    /// <example>Lorem ipsum dolor emet</example>
    public string Remarks { get; set; }
}