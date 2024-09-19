using SPTKnowledgeService.DTO;

namespace SPTKnowledgeService.Services.Impl
{
    public class SubjectService : ISubjectService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SubjectService> _logger;

        public SubjectService(ILogger<SubjectService> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
            _logger.LogInformation($"HTTP Client Base Address: {_httpClient.BaseAddress}");
        }

        public async Task<SubjectDTO> GetSubjectByCodeAsync(string subjectCode)
        {
            try
            {
                _logger.LogInformation($"Getting subject by code: {subjectCode}");
                var requestUri = $"subjects/{subjectCode}";
                _logger.LogInformation($"Request URI: {_httpClient.BaseAddress}{requestUri}");

                var response = await _httpClient.GetAsync(requestUri);
                _logger.LogInformation($"Response status code: {response.StatusCode}");

                response.EnsureSuccessStatusCode();

                var subject = await response.Content.ReadFromJsonAsync<SubjectDTO>();
                _logger.LogInformation($"Subject retrieved: {subject != null}");

                return subject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the subject by code.");
                throw;
            }
        }

        public async Task<bool> SubjectExistsAsync(string subjectCode)
        {
            try
            {
                _logger.LogInformation($"Checking if subject exists: {subjectCode}");
                var requestUri = $"subjects/Exists/{subjectCode}";
                _logger.LogInformation($"Request URI: {_httpClient.BaseAddress}{requestUri}");

                var response = await _httpClient.GetAsync(requestUri);
                _logger.LogInformation($"Response status code: {response.StatusCode}");

                response.EnsureSuccessStatusCode();

                var exists = await response.Content.ReadFromJsonAsync<bool>();
                _logger.LogInformation($"Subject exists: {exists}");

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if the subject exists.");
                throw;
            }
        }
    }
}
