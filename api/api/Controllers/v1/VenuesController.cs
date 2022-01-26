using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
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
}