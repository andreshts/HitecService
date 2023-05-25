using HitecService.Core.Services.IServices;
using HitecService.Models.Requests.User;
using Microsoft.AspNetCore.Mvc;

namespace HitecService.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
        => _userService = userService;

    [HttpGet("get_all")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userService.GetUsers();

        return Ok(Success(result));
    }

    [HttpGet("get_user")]
    public async Task<IActionResult> GetUserById([FromQuery] int id)
    {
        var result = await _userService.GetUserById(id);

        return result is null ? BadRequest(Fail("The user wasn't found")) : Ok(Success(result));
    }

    [HttpGet("get_users_rol_id")]
    public async Task<IActionResult> GetUsersByRolId([FromQuery] int id)
    {
        var result = await _userService.GetUsersByRolId(id);

        return Ok(Success(result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] RequestUpdateUser requestUpdateUser)
    {
        var result = await _userService.UpdateUser(id, requestUpdateUser);

        return result is null ? BadRequest(Fail("The user wasn't updated")) : Ok(Success(result));
    }

    [HttpPut("update_password_user/{id:int}")]
    public async Task<IActionResult> UpdatePasswordUser([FromRoute] int id, [FromBody] RequestUpdatePasswordUser requestUpdatePasswordUser)
    {
        var result = await _userService.UpdatePasswordUser(id, requestUpdatePasswordUser);

        return result is null ? BadRequest(Fail("The password user wasn't updated")) : Ok(Success(result));
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] RequestCreateUser requestCreateUser)
    {
        var result = await _userService.CreateUser(requestCreateUser);

        return result is null ? BadRequest(Fail("The user wasn't created")) : Ok(Success(result));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var result = await _userService.DeleteUser(id);

        return result is false ? BadRequest(Fail("The user wasn't deleted")) : Ok(Success(result));
    }
}