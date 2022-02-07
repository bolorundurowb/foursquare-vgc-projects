using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meerkat;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using neophyte.api.Core.Utilities;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Enums;
using neophyte.api.Data.Repositories.Interfaces;

namespace neophyte.api.Data.Repositories.Implementations;

public class VenueRepository : IVenueRepository
{
    public Task<List<Venue>> GetAll() =>
        Meerkat.Query<Venue>()
            .OrderBy(x => x.Name)
            .ToListAsync();

    public async Task<Dictionary<string, Venue>> FindAndMapById(IEnumerable<string> venueIds)
    {
        var ids = venueIds.Select(x => (object)ObjectId.Parse(x));
        var venues = await Meerkat.Query<Venue>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        return venues.ToDictionary(x => x.Id.ToString(), y => y);
    }

    public Task<Venue> FindById(string venueId) => Meerkat.FindByIdAsync<Venue>(ObjectId.Parse(venueId));

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

    public Task Remove(string venueId) => Meerkat.RemoveByIdAsync<Venue>(ObjectId.Parse(venueId));
}