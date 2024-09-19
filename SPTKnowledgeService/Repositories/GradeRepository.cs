
using SPTKnowledgeService.Models;

namespace SPTGradeService.Repositories
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetGradesAsync();
        Task<Grade> GetGradeByCodeAsync(string code);
        Task AddGradeAsync(Grade Grade);
        Task UpdateGradeAsync(Grade Grade);
        Task DeleteGradeAsync(string code);
        Task<bool> GradeExistsAsync(string code);
    }
}
