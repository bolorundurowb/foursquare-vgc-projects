using System.Collections.Generic;
using System.Threading.Tasks;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Enums;

namespace neophyte.api.Data.Repositories.Interfaces;

public interface IVenueRepository
{
    Task<List<Venue>> GetAll();

    Task<Dictionary<string, Venue>> FindAndMapById(IEnumerable<string> venueIds);

    Task<Venue> FindById(string venueId);

    Task<Venue> FindByName(string name);

    Task<Venue> Create(string name, List<(SeatCategory Category, string Range)> seatRanges);

    Task Remove(string venueId);

    Task RemoveSeat(string venueId, string seatNumber);
}