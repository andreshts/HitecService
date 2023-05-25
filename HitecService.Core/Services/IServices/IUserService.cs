using HitecService.Models.DTO.User;
using HitecService.Models.Requests.User;

namespace HitecService.Core.Services.IServices;

public interface IUserService
{
    Task<ICollection<UserDto>> GetUsers();
    Task<UserDto?> GetUserById(int                 id);
    Task<ICollection<UserDto>> GetUsersByRolId(int id);
    Task<UserDto?> UpdateUser(int                  id, RequestUpdateUser requestUpdateUser);
    Task<UserDto?> CreateUser(RequestCreateUser    requestCreateUser);
    Task<bool> DeleteUser(int                      id);
    Task<UserDto?> UpdatePasswordUser(int          id, RequestUpdatePasswordUser requestUpdatePasswordUser);
}