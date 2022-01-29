using System.Collections.Generic;

namespace api.Models.View;

public class EventViewModel : BaseEventViewModel
{
    public string RegistrationUrl { get; set; }

    public string RegistrationUrlQrCode { get; set; }

    public List<EventSeatViewModel> AvailableSeats { get; set; }
}