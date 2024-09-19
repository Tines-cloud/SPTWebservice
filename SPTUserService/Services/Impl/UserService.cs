using SPTUserService.DTO;
using SPTUserService.Mappers;
using SPTUserService.Models;
using SPTUserService.Repositories;

namespace SPTUserService.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                return users.Select(UserMapper.MapUserToUserDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting users.");
                throw new ApplicationException("Unable to get users.", ex);
            }
        }

        public async Task<UserDTO> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(username);
                return UserMapper.MapUserToUserDto(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the user with USERNAME {username}.");
                throw new ApplicationException($"Unable to get the user with USERNAME {username}.", ex);
            }
        }

        public async Task AddUserAsync(UserDTO userDto)
        {
            try
            {
                User user = UserMapper.MapUserDtoToUser(userDto);
                await _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new user.");
                throw new ApplicationException("Unable to add the new user.", ex);
            }
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            try
            {
                User user = UserMapper.MapUserDtoToUser(userDto);
                await _userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the user with USERNAME {userDto.Username}.");
                throw new ApplicationException($"Unable to update the user with USERNAME {userDto.Username}.", ex);
            }
        }

        public async Task DeleteUserAsync(string username)
        {
            try
            {
                await _userRepository.DeleteUserAsync(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the user with USERNAME {username}.");
                throw new ApplicationException($"Unable to delete the user with USERNAME {username}.", ex);
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            try
            {
                return await _userRepository.UserExistsAsync(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if the user with USERNAME {username} exists.");
                throw new ApplicationException($"Unable to check if the user with USERNAME {username} exists.", ex);
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(username);
                return user != null && user.Password == password;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                throw new ApplicationException("Unable to login.", ex);
            }
        }
    }
}
