using HitecService.Core.Services.IServices;
using HitecService.Models.Requests.Privileges;
using Microsoft.AspNetCore.Mvc;

namespace HitecService.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PrivilegeController : BaseController
{
    private readonly IPrivilegeService _privilegeService;

    public PrivilegeController(IPrivilegeService privilegeService)
        => _privilegeService = privilegeService;

    [HttpPost]
    public async Task<IActionResult> CreatePrivileges([FromBody] RequestPrivilege requestPrivilege)
    {
        var result = await _privilegeService.CreatePrivilege(requestPrivilege);

        return result is null ? BadRequest(Fail("The privileges wasn't created")) : Ok(Success(result));
    }

    [HttpGet("get_privilege_rol_id")]
    public async Task<IActionResult> GetPrivilegeById([FromQuery] int rolId)
    {
        var result = await _privilegeService.GetPrivilegesByRolId(rolId);

        return Ok(Success(result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePrivilege([FromRoute] int id, [FromBody] RequestPrivilege requestUpdatePrivilege)
    {
        var result = await _privilegeService.UpdatePrivilege(id, requestUpdatePrivilege);

        return result is null ? BadRequest(Fail("The privilege wasn't updated")) : Ok(Success(result));
    }

    [HttpGet("get_all")]
    public async Task<IActionResult> GetAllPrivileges()
    {
        var result = await _privilegeService.GetPrivileges();

        return Ok(Success(result));
    }
}