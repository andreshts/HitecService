using HitecService.Models.DTO.Privileges;
using HitecService.Models.Requests.Privileges;

namespace HitecService.Core.Services.IServices;

public interface IPrivilegeService
{
    Task<ICollection<PrivilegeMenuConfigurationDto>> GetPrivilegesByRolId(int rolId);
    Task<PrivilegesDto?> UpdatePrivilege(int                                  id, RequestPrivilege requestUpdatePrivilege);
    Task<PrivilegesDto?> CreatePrivilege(RequestPrivilege                     requestPrivilege);
    Task<ICollection<PrivilegesDto>> GetPrivileges();
}