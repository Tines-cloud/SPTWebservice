using System.ComponentModel.DataAnnotations;

namespace SPTUserService.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }

        public UserPreference UserPreference { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
