using HitecService.Core.Services.IServices;
using HitecService.Models.Requests.Rol;
using Microsoft.AspNetCore.Mvc;

namespace HitecService.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RoleController : BaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
        => _roleService = roleService;

    [HttpGet("get_all")]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _roleService.GetRoles();

        return Ok(Success(result));
    }

    [HttpGet("get_rol")]
    public async Task<IActionResult> GetRoleById([FromQuery] int id)
    {
        var result = await _roleService.GetRoleById(id);

        return result is null ? BadRequest(Fail("The rol wasn't found")) : Ok(Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RequestRole requestRole)
    {
        var result = await _roleService.CreateRole(requestRole);

        return result is null ? BadRequest(Fail("The rol wasn't created")) : Ok(Success(result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRole([FromRoute] int id, [FromBody] RequestRole request)
    {
        var result = await _roleService.UpdateRole(id, request);

        return result is null ? BadRequest(Fail("The user wasn't updated")) : Ok(Success(result));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRole([FromRoute] int id)
    {
        var result = await _roleService.DeleteRole(id);

        return result is false ? BadRequest(Fail("The rol wasn't deleted")) : Ok(Success(result));
    }
}