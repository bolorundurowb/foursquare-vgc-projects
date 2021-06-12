using System.Threading.Tasks;
using api.Configuration;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using api.Shared.Exceptions;
using api.Validators;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AuthController : ApiController
    {
        private readonly IAdminsRepository _adminsRepo;

        public AuthController(IMapper mapper, IAdminsRepository adminsRepo) : base(mapper)
        {
            _adminsRepo = adminsRepo;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthViewModel), 200)]
        [ProducesResponseType(typeof(GenericViewModel), 400)]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel bm)
        {
            var (isValid, errorMessages) = await IsValid<LoginBindingModelValidator, LoginBindingModel>(bm);

            if (!isValid)
            {
                return BadRequest(errorMessages);
            }

            try
            {
                var admin = await _adminsRepo.Login(bm.EmailAddress);
                var (token, expiry) = Helpers.GenerateToken(admin.Id.ToString(), admin.EmailAddress);

                return Ok(new AuthViewModel
                {
                    Token = token,
                    ExpiresAt = expiry,
                    Admin = Mapper.Map<AdminViewModel>(admin)
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}