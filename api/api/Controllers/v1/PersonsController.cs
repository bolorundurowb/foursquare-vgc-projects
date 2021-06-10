using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
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
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CheckPerson(string phoneNumber = null)
        {
            var person = await _personsRepo.GetByPhone(phoneNumber);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(_qrCodeService.CreateQrFromCode(person.Id.ToString()));
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Add([FromBody] PersonCreationBindingModel bm)
        {
            var (isValid, errorMessages) =
                await IsValid<PersonCreationBindingModelValidator, PersonCreationBindingModel>(bm);
            if (!isValid)
            {
                return BadRequest(errorMessages);
            }

            var person = await _personsRepo.Create(bm.FirstName, bm.LastName, bm.Phone);
            return Ok(_qrCodeService.CreateQrFromCode(person.Id.ToString()));
        }
    }
}