using SPTUserService.Models;

namespace SPTUserService.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string username);
        Task<bool> UserExistsAsync(string username);
    }
}
