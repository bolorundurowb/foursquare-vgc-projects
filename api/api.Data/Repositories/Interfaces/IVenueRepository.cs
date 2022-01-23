using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;

namespace api.Data.Repositories.Interfaces;

public interface IVenueRepository
{
    Task<Venue> Create(string name, List<string> seatRanges);
}