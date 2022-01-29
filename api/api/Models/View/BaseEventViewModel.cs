using System;

namespace api.Models.View;

public class BaseEventViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public int? NumOfAttendees { get; set; }
}