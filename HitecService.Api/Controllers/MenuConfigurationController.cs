using HitecService.Core.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HitecService.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MenuConfigurationController : BaseController
{
    private readonly IMenuConfigurationService _menuConfigurationService;

    public MenuConfigurationController(IMenuConfigurationService menuConfigurationService)
        => _menuConfigurationService = menuConfigurationService;

    [HttpGet("get_all")]
    public async Task<IActionResult> GetAllMenuConfigurations()
    {
        var result = await _menuConfigurationService.GetAllMenuConfigurations();

        return Ok(Success(result));
    }
}