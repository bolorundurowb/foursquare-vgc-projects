using System;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Models.View;

public class AdminViewModel
{
    /// <summary>
    /// The admins internal identifier
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [SwaggerSchema(Nullable = false)]
    public string Id { get; set; }

    /// <summary>
    /// The full name of the admin
    /// </summary>
    /// <example>John Doe</example>
    public string Name { get; set; }

    /// <summary>
    /// The admins email address
    /// </summary>
    /// <example>john@doe.com</example>
    [SwaggerSchema(Nullable = false)]
    public string EmailAddress { get; set; }

    /// <summary>
    /// Whether or not this admin is using the default password assigned to him/her
    /// </summary>
    /// <example>false</example>
    public bool IsUsingDefaultPassword { get; set; }

    /// <summary>
    /// The date and time this admins account was created
    /// </summary>
    /// <example>2022-02-04T08:28:10.519Z</example>
    public DateTime AddedAt { get; set; }
}