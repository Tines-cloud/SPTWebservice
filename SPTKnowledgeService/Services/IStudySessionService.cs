using SPTKnowledgeService.DTO;

namespace SPTKnowledgeService.Services
{
    public interface IStudySessionService
    {
        Task<IEnumerable<StudySessionDTO>> GetStudySessionsAsync();
        Task<IEnumerable<StudySessionDTO>> GetStudySessionByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate);
        Task AddStudySessionAsync(StudySessionDTO studySessionDTO);
        Task UpdateStudySessionAsync(StudySessionDTO studySessionDTO);
        Task DeleteStudySessionAsync(int id);
        Task<bool> StudySessionExistsAsync(int id);
        Task<StudySessionDTO> GetStudySessionByIdAsync(int id);
    }
}
