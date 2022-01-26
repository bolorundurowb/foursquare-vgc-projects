using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiVersion("1.0")]
public class VenuesController : ApiController
{
    private readonly IVenueRepository _venueRepo;

    public VenuesController(IMapper mapper, IVenueRepository venueRepo) : base(mapper)
    {
        _venueRepo = venueRepo;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<VenueViewModel>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var venues = await _venueRepo.GetAll();
        return Ok(Mapper.Map<List<VenueViewModel>>(venues));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(VenueViewModel), 201)]
    [ProducesResponseType(typeof(GenericViewModel), 409)]
    public async Task<IActionResult> Create([FromBody] VenueCreationBindingModel bm)
    {
        var venue = await _venueRepo.FindByName(bm.Name);

        if (venue != null)
            return Conflict("A venue exists with the same name.");

        var seatRanges = bm.SeatRanges
            .Select(x => (x.Category, x.Number))
            .ToList();
        venue = await _venueRepo.Create(bm.Name, seatRanges);
        return Ok(Mapper.Map<VenueViewModel>(venue));
    }
}