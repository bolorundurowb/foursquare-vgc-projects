using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PersonsController : ApiController
    {
        public PersonsController(IMapper mapper) : base(mapper)
        {
        }
    }
}