using HitecService.Data.Database.Models;

namespace HitecService.Data.Repositories.IRepositories;

public interface IPrivilegeRepository
{
    Task<List<Privilege>> GetAllPrivileges(int     rolId);
    Task<Privilege> UpdatePrivilegeAsync(Privilege privilege);
    Task<Privilege> CreatePrivilegeAsync(Privilege privilege);
    Task<Privilege?> GetPrivilegeByIdAsync(int     id);

    Task<List<Privilege>> GetPrivilegesAsync();
}