using SPTKnowledgeService.DTO;

namespace SPTKnowledgeService.Services
{
    public interface IKnowledgeService
    {
        Task<IEnumerable<KnowledgeDTO>> GetKnowledgesAsync();
        Task<IEnumerable<KnowledgeDTO>> GetKnowledgeByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate);
        Task AddKnowledgeAsync(KnowledgeDTO knowledgeDTO);
        Task UpdateKnowledgeAsync(KnowledgeDTO knowledgeDTO);
        Task DeleteKnowledgeAsync(int id);
        Task<bool> KnowledgeExistsAsync(int id);
        Task<KnowledgeDTO> GetKnowledgeByIdAsync(int id);
        Task<KnowledgeDTO> GetLatestKnowledgeAsync(string subjectCode, string username);
    }
}
