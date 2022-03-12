using System;

namespace neophyte.api.Models.View;

public class AuthViewModel
{
    /// <summary>
    /// The bearer auth token that grants authorized access to the systems
    /// </summary>
    /// <example>2022-02-04T08:28:10.519Z</example>
    public string Token { get; set; }

    /// <summary>
    /// The date and time the auth token expires
    /// </summary>
    /// <example>2022-02-04T08:28:10.519Z</example>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// The details of the authenticated amin
    /// </summary>
    public AdminViewModel Admin { get; set; }
}