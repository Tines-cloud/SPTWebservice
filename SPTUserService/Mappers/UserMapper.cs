using SPTUserService.DTO;
using SPTUserService.Models;

namespace SPTUserService.Mappers
{
    public static class UserMapper
    {
        public static UserDTO MapUserToUserDto(User user)
        {
            if (user == null) return null;

            return new UserDTO
            {
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                Role = user.Role != null ? new RoleDTO
                {
                    RoleName = user.Role.RoleName,
                    RoleId = user.Role.RoleId
                } : null,
                UserPreference = user.UserPreference != null ? new UserPreferenceDTO
                {
                    UserPreferenceId = user.UserPreference.UserPreferenceId,
                    DefFocusTime = user.UserPreference.DefFocusTime,
                    DefBreakTime = user.UserPreference.DefBreakTime,
                    Notifications = user.UserPreference.Notifications,
                    Username = user.UserPreference.Username
                } : null
            };
        }

        public static User MapUserDtoToUser(UserDTO userDto)
        {
            if (userDto == null) return null;

            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                FirstName = userDto.FirstName,
                RoleId = userDto.Role != null ? userDto.Role.RoleId : default(int), // only set RoleId
                UserPreference = userDto.UserPreference != null ? new UserPreference
                {
                    DefFocusTime = userDto.UserPreference.DefFocusTime,
                    DefBreakTime = userDto.UserPreference.DefBreakTime,
                    Notifications = userDto.UserPreference.Notifications,
                    Username = userDto.UserPreference.Username
                } : null
            };

            return user;
        }
    }
}
