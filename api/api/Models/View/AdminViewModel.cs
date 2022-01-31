using System;

namespace api.Models.View;

public class AdminViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public bool IsUsingDefaultPassword { get; set; }

    public DateTime AddedAt { get; set; }
}