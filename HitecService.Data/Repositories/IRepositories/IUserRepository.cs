using HitecService.Data.Database.Models;

namespace HitecService.Data.Repositories.IRepositories;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int         id);
    Task<List<User>> GetUserByRolIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string   email);
    Task<User> UpdateUserAsync(User          user);
    Task<User> CreateUserAsync(User          user);

    Task<bool> DeleteUserAsync(User user);
    //Task<User> UpdatePasswordUserAsync(User user);
}