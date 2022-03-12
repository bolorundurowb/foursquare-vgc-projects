using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;
using neophyte.api.Shared.Media.Implementations;
using Swashbuckle.AspNetCore.Annotations;

namespace neophyte.api.Controllers.v1;

[ApiVersion("1.0")]
public class PersonsController : ApiController
{
    private readonly IPersonsRepository _personsRepo;

    public PersonsController(IMapper mapper, IPersonsRepository personsRepo) :
        base(mapper)
    {
        _personsRepo = personsRepo;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<BasePersonViewModel>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] string name = null)
    {
        var persons = await _personsRepo.GetAll(name);
        return Ok(Mapper.Map<List<PersonViewModel>>(persons));
    }

    [AllowAnonymous]
    [HttpPost("")]
    [ProducesResponseType(typeof(PersonViewModel), 201)]
    [ProducesResponseType(typeof(GenericViewModel), 400)]
    public async Task<IActionResult> Add([FromBody] PersonCreationBindingModel bm)
    {
        var person = await _personsRepo.Create(bm.FirstName, bm.LastName, bm.Phone);
        var response = Mapper.Map<PersonViewModel>(person);
        response.QrUrl = QrCodeService.GenerateQrCode(person.Id.ToString());

        return Created(response);
    }

    [AllowAnonymous]
    [HttpGet("check")]
    [ProducesResponseType(typeof(PersonViewModel), 200)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckPerson(string phoneNumber = null)
    {
        var person = await _personsRepo.GetByPhone(phoneNumber);

        if (person == null)
            return NotFound();

        var response = Mapper.Map<PersonViewModel>(person);
        response.QrUrl = QrCodeService.GenerateQrCode(person.Id.ToString());

        return Ok(response);
    }
}