using HitecService.Data.Database.Models;
using HitecService.Models.DTO.Rol;

namespace HitecService.Data.Repositories.IRepositories;

public interface IRoleRepository
{
    Task<List<RoleDto>> GetRolesAsync();
    Task<Role?> GetRoleByIdAsync(int id);
    Task<bool> IsExistRoleAsync(int  id);
    Task<Role> UpdateRoleAsync(Role  role);
    Task<Role> CreateRoleAsync(Role  role);
    Task<bool> DeleteRoleAsync(Role  role);
}