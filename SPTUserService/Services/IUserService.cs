using SPTUserService.DTO;

namespace SPTUserService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByUsernameAsync(string username);
        Task AddUserAsync(UserDTO user);
        Task UpdateUserAsync(UserDTO user);
        Task DeleteUserAsync(string username);
        Task<bool> UserExistsAsync(string username);
        Task<bool> LoginAsync(string username, string password);
    }
}
