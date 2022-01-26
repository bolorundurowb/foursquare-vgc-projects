using System.Collections.Generic;
using System.Threading.Tasks;
using api.Core.Utilities;
using api.Data.Entities;
using api.Data.Enums;
using api.Data.Repositories.Interfaces;

namespace api.Data.Repositories.Implementations;

public class VenueRepository : IVenueRepository
{
    public async Task<Venue> Create(string name, List<(SeatCategory Category, string Range)> seatRanges)
    {
        var venue = new Venue(name);

        foreach ((var category, var range) in seatRanges)
        {
            var seatValues = SeatRange.Parse(range);
            foreach (var seatValue in seatValues)
            {
                venue.AddSeat(category, seatValue);
            }
        }

        await venue.SaveAsync();

        return venue;
    }
}