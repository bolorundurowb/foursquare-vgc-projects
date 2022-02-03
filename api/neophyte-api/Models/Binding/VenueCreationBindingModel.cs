using System.Collections.Generic;

namespace neophyte;

public class VenueCreationBindingModel
{
    public string Name { get; set; }

    public List<SeatRangeBindingModel> SeatRanges { get; set; }
}