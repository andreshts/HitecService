using HitecService.Data.Database.Models;

namespace HitecService.Data.Repositories.IRepositories;

public interface IMenuConfigurationRepository
{
    Task<List<MenuConfiguration>> GetAllMenuConfigurationsAsync();
    Task<bool> IsExistMenuConfigurationAsync(int id);
}