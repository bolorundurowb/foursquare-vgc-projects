using System;
using System.Collections.Generic;

namespace api.Models.View;

public class EventViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public string RegistrationUrl { get; set; }

    public string RegistrationUrlQrCode { get; set; }

    public List<EventSeatViewModel> AvailableSeats { get; set; }
}