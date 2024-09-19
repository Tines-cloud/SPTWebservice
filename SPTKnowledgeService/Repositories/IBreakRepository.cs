using SPTKnowledgeService.Models;

namespace SPTKnowledgeService.Repositories
{
    public interface IBreakRepository
    {
        Task<IEnumerable<Break>> GetBreaksAsync();
        Task<IEnumerable<Break>> GetBreakByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate);
        Task AddBreakAsync(Break Break);
        Task UpdateBreakAsync(Break Break);
        Task DeleteBreakAsync(int id);
        Task<bool> BreakExistsAsync(int id);
        Task<Break> GetBreakByIdAsync(int id);
    }
}
