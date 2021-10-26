using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Shared.Media.Interfaces;
using api.Validators;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class PersonsController : ApiController
    {
        private readonly IPersonsRepository _personsRepo;
        private readonly IQrCodeService _qrCodeService;

        public PersonsController(IMapper mapper, IPersonsRepository personsRepo, IQrCodeService qrCodeService) :
            base(mapper)
        {
            _personsRepo = personsRepo;
            _qrCodeService = qrCodeService;
        }

        [HttpGet("check")]
        [ProducesResponseType(typeof(PersonViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CheckPerson(string phoneNumber = null)
        {
            var person = await _personsRepo.GetByPhone(phoneNumber);

            if (person == null)
            {
                return NotFound();
            }

            var response = Mapper.Map<PersonViewModel>(person);
            response.QrUrl = _qrCodeService.CreateQrFromCode(person.Id.ToString());

            return Ok(response);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(PersonViewModel), 201)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        public async Task<IActionResult> Add([FromBody] PersonCreationBindingModel bm)
        {
            var (isValid, errorMessages) =
                await IsValid<PersonCreationBindingModelValidator, PersonCreationBindingModel>(bm);

            if (!isValid)
            {
                return BadRequest(errorMessages);
            }

            var person = await _personsRepo.Create(bm.FirstName, bm.LastName, bm.Phone);

            var response = Mapper.Map<PersonViewModel>(person);
            response.QrUrl = _qrCodeService.CreateQrFromCode(person.Id.ToString());

            return Created(response);
        }
    }
}