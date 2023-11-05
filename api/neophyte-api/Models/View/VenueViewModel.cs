using System.Collections.Generic;

namespace neophyte.api.Models.View;

public class VenueViewModel : BaseVenueViewModel
{
    /// <summary>
    /// The seats in the venue
    /// </summary>
    public List<SeatViewModel> Seats { get; set; } = new();
}