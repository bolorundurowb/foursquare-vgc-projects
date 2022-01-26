using api.Data.Repositories.Interfaces;
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
}