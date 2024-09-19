
using SPTUserService.Models;

namespace SPTUserService.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public RoleDTO Role { get; set; }
        public UserPreferenceDTO UserPreference { get; set; }
    }
}
