using System.Threading.Tasks;
using api.Configuration;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v2;

[ApiVersion("2.0")]
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
    public async Task<IActionResult> Login([FromBody] LoginV2BindingModel bm)
    {
        var admin = await _adminsRepo.FindByEmail(bm.EmailAddress);

        if (admin == null)
            return NotFound("Admin account not found.");

        if (!admin.VerifyPassword(bm.Password))
            return BadRequest("Incorrect password.");

        var (token, expiry) = Helpers.GenerateToken(admin.Id.ToString(), admin.EmailAddress);

        return Ok(new AuthViewModel
        {
            Token = token,
            ExpiresAt = expiry,
            Admin = Mapper.Map<AdminViewModel>(admin)
        });
    }
}