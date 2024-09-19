namespace SPTUserService.Models
{
    public class UserPreferenceDTO
    {
        public int UserPreferenceId { get; set; }

        public int DefFocusTime { get; set; }

        public int DefBreakTime { get; set; }

        public bool Notifications { get; set; }

        public string Username { get; set; }

    }
}
