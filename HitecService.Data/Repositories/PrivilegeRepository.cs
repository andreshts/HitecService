using HitecService.Data.Database;
using HitecService.Data.Database.Models;
using HitecService.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HitecService.Data.Repositories;

public class PrivilegeRepository : BaseRepository, IPrivilegeRepository
{
    public PrivilegeRepository(HitecServiceDbContext context) : base(context) { }

    public Task<List<Privilege>> GetAllPrivileges(int rolId)
        => Context.Privileges.Where(x => x.RolId == rolId).ToListAsync();


    public Task<Privilege?> GetPrivilegeByIdAsync(int id)
        => Context.Privileges.FirstOrDefaultAsync(x => x.Id == id);


    public Task<List<Privilege>> GetPrivilegesAsync()
        => Context.Privileges.ToListAsync();

    public async Task<Privilege> CreatePrivilegeAsync(Privilege privilege)
    {
        var today = DateTime.Now;
        privilege.CreatedAt = today;
        privilege.UpdatedAt = today;

        await Context.Privileges.AddAsync(privilege);
        await Context.SaveChangesAsync();

        return privilege;
    }

    public async Task<Privilege> UpdatePrivilegeAsync(Privilege privilege)
    {
        Context.Privileges.Update(privilege);
        await Context.SaveChangesAsync();

        return privilege;
    }
}