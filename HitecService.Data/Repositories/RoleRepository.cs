using HitecService.Data.Database;
using HitecService.Data.Database.Models;
using HitecService.Data.Repositories.IRepositories;
using HitecService.Models.DTO.Rol;
using Microsoft.EntityFrameworkCore;

namespace HitecService.Data.Repositories;

public class RoleRepository : BaseRepository, IRoleRepository
{
    public RoleRepository(HitecServiceDbContext context) : base(context) { }

    public Task<bool> IsExistRoleAsync(int id)
        => Context.Roles.AnyAsync(x => x.Id == id);

    public Task<List<RoleDto>> GetRolesAsync()
        => Context.Roles.Include(x => x.Users).Include(x => x.Privileges).Select(s => new RoleDto
        {
            Id              = s.Id,
            Name            = s.Name,
            Description     = s.Description ?? string.Empty,
            AssociatedUsers = s.Users.Count(),
            Permissions     = s.Privileges.Count()
        }).ToListAsync();


    public Task<Role?> GetRoleByIdAsync(int id)
        => Context.Roles.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<bool> DeleteRoleAsync(Role role)
    {
        role.DeletedAt = DateTime.Now;
        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<Role> UpdateRoleAsync(Role role)
    {
        Context.Roles.Update(role);
        await Context.SaveChangesAsync();

        return role;
    }

    public async Task<Role> CreateRoleAsync(Role role)
    {
        await Context.AddAsync(role);
        await Context.SaveChangesAsync();

        return role;
    }
}