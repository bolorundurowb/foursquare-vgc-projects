﻿using System.Collections.Generic;

namespace api.Models.View;

public class VenueViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public List<SeatViewModel> Seats { get; set; }
}