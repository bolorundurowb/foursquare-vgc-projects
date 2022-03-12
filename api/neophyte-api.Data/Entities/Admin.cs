using System;
using meerkat;
using meerkat.Attributes;

namespace neophyte.api.Data.Entities;

[Collection(TrackTimestamps = true), IgnoreUnknownFields]
public class Admin : Schema
{
    public string Name { get; private set; }

    public string EmailAddress { get; private set; }

    public string PasswordHash { get; private set; }

    public bool IsUsingDefaultPassword { get; private set; }

    public DateTime AddedAt { get; private set; }

    private Admin()
    {
    }

    public Admin(string name, string emailAddress)
    {
        AddedAt = DateTime.UtcNow;
        Name = name;
        EmailAddress = emailAddress;
    }

    public void SetPassword(string password, bool isDefaultPassword = false)
    {
        IsUsingDefaultPassword = isDefaultPassword;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool VerifyPassword(string password) =>
        !string.IsNullOrWhiteSpace(password) && BCrypt.Net.BCrypt.Verify(password, PasswordHash);
}