using SPTKnowledgeService.Models;

namespace SPTKnowledgeService.Repositories
{
    public interface IKnowledgeRepository
    {
        Task<IEnumerable<Knowledge>> GetKnowledgesAsync();
        Task<IEnumerable<Knowledge>> GetKnowledgeByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate);
        Task AddKnowledgeAsync(Knowledge knowledge);
        Task UpdateKnowledgeAsync(Knowledge knowledge);
        Task DeleteKnowledgeAsync(int id);
        Task<bool> KnowledgeExistsAsync(int id);
        Task<Knowledge> GetKnowledgeByIdAsync(int id);
        Task<Knowledge> GetLatestKnowledgeAsync(string subjectCode, string username);
    }
}
