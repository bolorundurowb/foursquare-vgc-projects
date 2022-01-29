using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Shared.Media.Implementations;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

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
    [ProducesResponseType(404)]
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