using SPTKnowledgeService.Models;

namespace SPTKnowledgeService.Repositories
{
    public interface IStudySessionRepository
    {
        Task<IEnumerable<StudySession>> GetStudySessionsAsync();
        Task<IEnumerable<StudySession>> GetStudySessionByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate);
        Task AddStudySessionAsync(StudySession StudySession);
        Task UpdateStudySessionAsync(StudySession StudySession);
        Task DeleteStudySessionAsync(int id);
        Task<bool> StudySessionExistsAsync(int id);
        Task<StudySession> GetStudySessionByIdAsync(int id);
    }
}
