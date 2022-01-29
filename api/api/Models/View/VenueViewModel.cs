using System.Collections.Generic;

namespace api.Models.View;

public class VenueViewModel : BaseVenueViewModel
{
    public List<SeatViewModel> Seats { get; set; }
}