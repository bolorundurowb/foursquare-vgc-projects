using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Controllers.v1;

[ApiVersion("1.0")]
public class VenuesController : ApiController
{
    private readonly IVenueRepository _venueRepo;

    public VenuesController(IMapper mapper, IVenueRepository venueRepo) : base(mapper) => _venueRepo = venueRepo;

    [HttpGet("")]
    [ProducesResponseType(typeof(List<BaseVenueViewModel>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var venues = await _venueRepo.GetAll();
        return Ok(Mapper.Map<List<BaseVenueViewModel>>(venues));
    }

    [HttpGet("{venueId}")]
    [ProducesResponseType(typeof(VenueViewModel), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    public async Task<IActionResult> GetOne(string venueId)
    {
        var venue = await _venueRepo.FindById(venueId);

        if (venue == null)
            return NotFound("Venue not found.");

        return Ok(Mapper.Map<VenueViewModel>(venue));
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
            .Select(x => (x.Category, Number: x.NumberRange))
            .ToList();
        venue = await _venueRepo.Create(bm.Name, seatRanges);

        return Ok(Mapper.Map<VenueViewModel>(venue));
    }

    [HttpDelete("{venueId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Venue removed successfully.", typeof(GenericViewModel))]
    public async Task<IActionResult> Remove(string venueId)
    {
        await _venueRepo.Remove(venueId);
        return Ok("Venue removed successfully.");
    }
}