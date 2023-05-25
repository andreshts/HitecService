using AutoMapper;
using HitecService.Core.Services.IServices;
using HitecService.Data.Database.Models;
using HitecService.Data.Repositories.IRepositories;
using HitecService.Models.DTO.User;
using HitecService.Models.Requests.User;
using HitecService.Utility.Extensions;

namespace HitecService.Core.Services;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IMapper mapper) : base(mapper)
        => _userRepository = userRepository;

    public async Task<ICollection<UserDto>> GetUsers()
    {
        var users = await _userRepository.GetUsersAsync();

        return Mapper.Map<ICollection<UserDto>>(users);
    }

    public async Task<ICollection<UserDto>> GetUsersByRolId(int id)
    {
        var users = await _userRepository.GetUserByRolIdAsync(id);

        return Mapper.Map<ICollection<UserDto>>(users);
    }

    public async Task<UserDto?> UpdateUser(int id, RequestUpdateUser requestUpdateUser)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user is null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email))
            return null;

        user.UserName    = requestUpdateUser.UserName ?? "";
        user.Name        = requestUpdateUser.Name;
        user.LastName    = requestUpdateUser.LastName;
        user.Email       = requestUpdateUser.Email ?? "";
        user.RolId       = requestUpdateUser.RolId;
        user.Phone       = requestUpdateUser.Phone;
        user.Address     = requestUpdateUser.Address;
        user.Observation = requestUpdateUser.Observation;
        user.Status      = requestUpdateUser.Status;

        user = await _userRepository.UpdateUserAsync(user);

        return Mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdatePasswordUser(int id, RequestUpdatePasswordUser requestUpdatePasswordUser)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user is null)
            return null;

        var passwordEncrypt = CommonExtensions.ValidatePass(user.Password, requestUpdatePasswordUser.Password);

        if (!passwordEncrypt)
            return null;


        user.Password = CommonExtensions.EncryptPass(requestUpdatePasswordUser.NewPassword);

        user = await _userRepository.UpdateUserAsync(user);

        return Mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> CreateUser(RequestCreateUser requestCreateUser)
    {
        var passwordEncrypt = CommonExtensions.EncryptPass(requestCreateUser.Password);

        if (string.IsNullOrEmpty(passwordEncrypt))
            return null;

        requestCreateUser.Password = passwordEncrypt;
        var user = Mapper.Map<User>(requestCreateUser);
        user = await _userRepository.CreateUserAsync(user);

        return Mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user is null)
            return false;

        var result = await _userRepository.DeleteUserAsync(user);

        return result;
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        return Mapper.Map<UserDto>(user);
    }
}