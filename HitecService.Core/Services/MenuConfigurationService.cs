using AutoMapper;
using HitecService.Core.Services.IServices;
using HitecService.Data.Repositories.IRepositories;
using HitecService.Models.DTO.MenuConfiguration;

namespace HitecService.Core.Services;

public class MenuConfigurationService : BaseService, IMenuConfigurationService
{
    private readonly IMenuConfigurationRepository _menuConfigurationRepository;

    public MenuConfigurationService(IMenuConfigurationRepository menuConfigurationRepository, IMapper mapper)
        : base(mapper) => _menuConfigurationRepository = menuConfigurationRepository;

    public async Task<ICollection<MenuConfigurationDto>> GetAllMenuConfigurations()
    {
        var menuConfiguration = await _menuConfigurationRepository.GetAllMenuConfigurationsAsync();

        return Mapper.Map<ICollection<MenuConfigurationDto>>(menuConfiguration);
    }

    public Task<bool> IsExistMenuConfigurationById(int id)
        => _menuConfigurationRepository.IsExistMenuConfigurationAsync(id);
}