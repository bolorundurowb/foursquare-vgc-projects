using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v2
{
    [ApiVersion("2")]
    public class NewcomersController : ApiController
    {
        private readonly INewcomersRepository _newcomersRepo;

        public NewcomersController(IMapper mapper, INewcomersRepository newcomersRepo) : base(mapper)
        {
            _newcomersRepo = newcomersRepo;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(NewcomerViewModel), 201)]
        public async Task<IActionResult> AddNewcomer([FromBody] NewcomerV2BindingModel bm)
        {
            var newcomer = await _newcomersRepo.AddNewcomer(bm.FullName, bm.HomeAddress, bm.Phone, bm.EmailAddress,
                bm.BirthDay, bm.AgeGroup, bm.CommentsOrPrayers, bm.HowYouFoundUs, bm.BecomeMember);
            return Created(Mapper.Map<NewcomerViewModel>(newcomer));
        }
    }
}