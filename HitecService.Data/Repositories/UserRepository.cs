using HitecService.Data.Database;
using HitecService.Data.Database.Models;
using HitecService.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HitecService.Data.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(HitecServiceDbContext context) : base(context) { }

    public async Task<bool> DeleteUserAsync(User user)
    {
        user.DeletedAt = DateTime.Now;

        Context.Users.Update(user);
        await Context.SaveChangesAsync();

        return true;
    }

    public Task<List<User>> GetUsersAsync()
        => Context.Users.Include(x => x.Rol).ToListAsync();


    public Task<User?> GetUserByEmailAsync(string email)
        => Context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public Task<User?> GetUserByIdAsync(int id)
        => Context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<User>> GetUserByRolIdAsync(int id)
        => Context.Users.Where(x => x.RolId == id).ToListAsync();


    public async Task<User> UpdateUserAsync(User user)
    {
        user.UpdatedAt = DateTime.Now;
        Context.Users.Update(user);
        await Context.SaveChangesAsync();

        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var today = DateTime.Now;
        user.CreatedAt = today;
        user.UpdatedAt = today;

        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();

        return user;
    }
}