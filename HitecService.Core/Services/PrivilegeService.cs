using AutoMapper;
using HitecService.Core.Services.IServices;
using HitecService.Data.Database.Models;
using HitecService.Data.Repositories.IRepositories;
using HitecService.Models.DTO.Privileges;
using HitecService.Models.Requests.Privileges;

namespace HitecService.Core.Services;

public class PrivilegeService : BaseService, IPrivilegeService
{
    private readonly IMenuConfigurationRepository _menuConfigurationRepository;
    private readonly IPrivilegeRepository         _privilegeRepository;

    public PrivilegeService(IPrivilegeRepository privilegeRepository, IMenuConfigurationRepository menuConfigurationRepository, IMapper mapper) : base(mapper)
    {
        _privilegeRepository         = privilegeRepository;
        _menuConfigurationRepository = menuConfigurationRepository;
    }


    public async Task<PrivilegesDto?> CreatePrivilege(RequestPrivilege requestPrivilege)
    {
        var privilege = Mapper.Map<Privilege>(requestPrivilege);
        privilege = await _privilegeRepository.CreatePrivilegeAsync(privilege);

        return Mapper.Map<PrivilegesDto>(privilege);
    }


    public async Task<ICollection<PrivilegeMenuConfigurationDto>> GetPrivilegesByRolId(int rolId)
    {
        var menuConfigurations    = await _menuConfigurationRepository.GetAllMenuConfigurationsAsync();
        var privileges            = await _privilegeRepository.GetAllPrivileges(rolId);
        var menuConfigurationsDto = new List<PrivilegeMenuConfigurationDto>();
        if (menuConfigurations is { Count: > 0 })
            menuConfigurationsDto = menuConfigurations.Select(x => CreateMenuConfigurationsCustomList(x, privileges)).ToList();

        return menuConfigurationsDto;
    }

    public async Task<PrivilegesDto?> UpdatePrivilege(int id, RequestPrivilege requestUpdatePrivilege)
    {
        var privilege = await _privilegeRepository.GetPrivilegeByIdAsync(id);

        if (privilege is null)
            return null;

        privilege.CanCreate = requestUpdatePrivilege.CanCreate;
        privilege.CanEdit   = requestUpdatePrivilege.CanEdit;
        privilege.CanRead   = requestUpdatePrivilege.CanRead;
        privilege.CanDelete = requestUpdatePrivilege.CanDelete;

        privilege = await _privilegeRepository.UpdatePrivilegeAsync(privilege);

        return Mapper.Map<PrivilegesDto>(privilege);
    }

    public async Task<ICollection<PrivilegesDto>> GetPrivileges()
    {
        var users = await _privilegeRepository.GetPrivilegesAsync();

        return Mapper.Map<ICollection<PrivilegesDto>>(users);
    }

    private PrivilegeMenuConfigurationDto CreateMenuConfigurationsCustomList(
        MenuConfiguration menuConfiguration, ICollection<Privilege> privileges)
    {
        var menuConfigurationDto = Mapper.Map<PrivilegeMenuConfigurationDto>(menuConfiguration);
        var privilege            = privileges.FirstOrDefault(x => x.MenuConfigurationId == menuConfiguration.Id);

        if (privilege is null)
            return menuConfigurationDto;

        menuConfigurationDto.PrivilegeId = privilege.Id;
        menuConfigurationDto.CanCreate   = privilege.CanCreate;
        menuConfigurationDto.CanDelete   = privilege.CanDelete;
        menuConfigurationDto.CanEdit     = privilege.CanEdit;
        menuConfigurationDto.CanRead     = privilege.CanRead;

        return menuConfigurationDto;
    }
}