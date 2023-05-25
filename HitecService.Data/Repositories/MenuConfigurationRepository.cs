using HitecService.Data.Database;
using HitecService.Data.Database.Models;
using HitecService.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HitecService.Data.Repositories;

public class MenuConfigurationRepository : BaseRepository, IMenuConfigurationRepository
{
    public MenuConfigurationRepository(HitecServiceDbContext context) : base(context) { }

    public Task<bool> IsExistMenuConfigurationAsync(int id)
        => Context.MenuConfigurations.AnyAsync(x => x.Id == id);


    public Task<List<MenuConfiguration>> GetAllMenuConfigurationsAsync()
        => Context.MenuConfigurations.AsNoTracking().ToListAsync();

    public Task<MenuConfiguration> GetMenuConfigurationByIdAsync(int id)
        => Context.MenuConfigurations.FirstAsync(x => x.Id == id);

    public async Task<MenuConfiguration> CreateMenuConfigurationAsync(MenuConfiguration menuConfiguration)
    {
        await Context.AddAsync(menuConfiguration);
        await Context.SaveChangesAsync();

        return menuConfiguration;
    }

    public async Task<bool> DeleteMenuConfigurationAsync(MenuConfiguration menuConfiguration)
    {
        menuConfiguration.DeletedAt = DateTime.Now;

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<MenuConfiguration> UpdateMenuConfigurationAsync(MenuConfiguration menuConfiguration)
    {
        Context.MenuConfigurations.Update(menuConfiguration);
        await Context.SaveChangesAsync();

        return menuConfiguration;
    }
}