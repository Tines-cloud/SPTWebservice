namespace SPTUserService.Models
{
    public class UserPreference
    {
        public int UserPreferenceId { get; set; }

        public int DefFocusTime { get; set; }

        public int DefBreakTime { get; set; }

        public bool Notifications { get; set; }

        public string Username { get; set; }

        public User User { get; set; }

    }
}
