using HitecService.Models.DTO.Rol;
using HitecService.Models.Requests.Rol;

namespace HitecService.Core.Services.IServices;

public interface IRoleService
{
    Task<List<RoleDto>> GetRoles();
    Task<RoleDto?> GetRoleById(int        id);
    Task<bool> IsExistRoleById(int        id);
    Task<RoleDto?> UpdateRole(int         id, RequestRole request);
    Task<RoleDto?> CreateRole(RequestRole requestRole);
    Task<bool> DeleteRole(int             id);
}