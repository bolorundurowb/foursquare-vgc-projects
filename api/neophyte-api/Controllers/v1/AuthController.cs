using System;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Configuration;
using neophyte.api.Data.Repositories.Interfaces;
using neophyte.api.Models.Binding;
using neophyte.api.Models.View;

namespace neophyte.api.Controllers.v1;

[ApiVersion("1.0")]
public class AuthController : ApiController
{
    private readonly IAdminsRepository _adminsRepo;

    public AuthController(IMapper mapper, IAdminsRepository adminsRepo) : base(mapper)
    {
        _adminsRepo = adminsRepo;
    }

    [Obsolete]
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 400)]
    public async Task<IActionResult> Login([FromBody] LoginBindingModel bm)
    {
        var admin = await _adminsRepo.FindByEmail(bm.EmailAddress);

        if (admin == null)
            return NotFound("Admin account not found.");

        var (token, expiry) = Helpers.GenerateToken(admin.Id.ToString(), admin.EmailAddress);

        return Ok(new AuthViewModel
        {
            Token = token,
            ExpiresAt = expiry,
            Admin = Mapper.Map<AdminViewModel>(admin)
        });
    }
}