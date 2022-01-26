using System;

namespace api.Models.View;

public class EventViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public string RegistrationUrl { get; set; }

    public string RegistrationUrlQrCode { get; set; }
}