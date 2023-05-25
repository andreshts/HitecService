using AutoMapper;
using HitecService.Core.Services.IServices;
using HitecService.Data.Database.Models;
using HitecService.Data.Repositories.IRepositories;
using HitecService.Models.DTO.Rol;
using HitecService.Models.Requests.Rol;

namespace HitecService.Core.Services;

public class RoleService : BaseService, IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository, IMapper mapper) : base(mapper)
        => _roleRepository = roleRepository;

    public Task<List<RoleDto>> GetRoles()
        => _roleRepository.GetRolesAsync();

    public async Task<RoleDto?> GetRoleById(int id)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);

        return Mapper.Map<RoleDto>(role);
    }

    public Task<bool> IsExistRoleById(int id)
        => _roleRepository.IsExistRoleAsync(id);


    public async Task<RoleDto?> UpdateRole(int id, RequestRole request)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);

        if (role is null)
            return null;

        role.Name        = request.Name;
        role.Description = request.Description;

        role = await _roleRepository.UpdateRoleAsync(role);

        return Mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto?> CreateRole(RequestRole requestRole)
    {
        var role = Mapper.Map<Role>(requestRole);
        role = await _roleRepository.CreateRoleAsync(role);

        return Mapper.Map<RoleDto>(role);
    }

    public async Task<bool> DeleteRole(int id)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);

        if (role is null)
            return false;

        var result = await _roleRepository.DeleteRoleAsync(role);

        return result;
    }
}