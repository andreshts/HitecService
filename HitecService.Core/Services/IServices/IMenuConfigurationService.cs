using HitecService.Models.DTO.MenuConfiguration;

namespace HitecService.Core.Services.IServices;

public interface IMenuConfigurationService
{
    Task<ICollection<MenuConfigurationDto>> GetAllMenuConfigurations();
    Task<bool> IsExistMenuConfigurationById(int id);
}