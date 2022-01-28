using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Core.Utilities;
using api.Data.Entities;
using api.Data.Enums;
using api.Data.Repositories.Interfaces;
using meerkat;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api.Data.Repositories.Implementations;

public class VenueRepository : IVenueRepository
{
    public Task<List<Venue>> GetAll() =>
        Meerkat.Query<Venue>()
            .OrderBy(x => x.Name)
            .ToListAsync();

    public async Task<Dictionary<string, Venue>> GetAll(IEnumerable<string> venueIds)
    {
        var ids = venueIds.Select(x => (object)x);
        var venues = await Meerkat.Query<Venue>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        return venues.ToDictionary(x => x.Id.ToString(), y => y);
    }

    public Task<Venue> FindByName(string name) => Meerkat.FindOneAsync<Venue>(x => x.Name == name);

    public async Task<Venue> Create(string name, List<(SeatCategory Category, string Range)> seatRanges)
    {
        var venue = new Venue(name);

        foreach (var (category, range) in seatRanges)
        {
            var seatValues = SeatRange.Parse(range);
            foreach (var seatValue in seatValues) venue.AddSeat(category, seatValue);
        }

        await venue.SaveAsync();

        return venue;
    }
}