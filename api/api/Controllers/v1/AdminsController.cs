using System.Collections.Generic;
using System.Threading.Tasks;
using api.Configuration;
using api.Data.Repositories.Interfaces;
using api.Models.Binding;
using api.Models.View;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiVersion("1.0")]
public class AdminsController : ApiController
{
    private readonly IAdminsRepository _adminsRepo;

    public AdminsController(IMapper mapper, IAdminsRepository adminsRepo) : base(mapper)
    {
        _adminsRepo = adminsRepo;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<AdminViewModel>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var admins = await _adminsRepo.GetAll();
        return Ok(Mapper.Map<List<AdminViewModel>>(admins));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(AdminViewModel), 201)]
    [ProducesResponseType(typeof(GenericViewModel), 400)]
    [ProducesResponseType(typeof(GenericViewModel), 409)]
    public async Task<IActionResult> Create([FromBody] CreateAdminBindingModel bm)
    {
        var admin = await _adminsRepo.FindByEmail(bm.EmailAddress);

        if (admin != null)
            return Conflict("Admin already exists.");

        admin = await _adminsRepo.Create(bm.Name, bm.EmailAddress, bm.Password);
        return Created(Mapper.Map<AdminViewModel>(admin));
    }

    [HttpPut("current/password")]
    [ProducesResponseType(typeof(GenericViewModel), 200)]
    [ProducesResponseType(typeof(GenericViewModel), 400)]
    [ProducesResponseType(typeof(GenericViewModel), 404)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordBindingModel bm)
    {
        var adminId = User.GetUserId();
        var admin = await _adminsRepo.FindById(adminId);

        if (admin == null)
            return NotFound("Admin account not found.");

        await _adminsRepo.UpdatePassword(admin, bm.Password);
        return Ok("Password updated successfully.");
    }
}