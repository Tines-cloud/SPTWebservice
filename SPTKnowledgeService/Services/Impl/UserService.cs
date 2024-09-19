namespace SPTKnowledgeService.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
            _logger.LogInformation($"HTTP Client Base Address: {_httpClient.BaseAddress}");
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var response = await _httpClient.GetAsync($"users/Exists/{username}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
