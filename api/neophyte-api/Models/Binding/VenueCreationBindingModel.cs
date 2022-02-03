using System.Collections.Generic;

namespace neophyte.api.Models.Binding;

public class VenueCreationBindingModel
{
    public string Name { get; set; }

    public List<SeatRangeBindingModel> SeatRanges { get; set; }
}