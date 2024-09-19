using SPTKnowledgeService.DTO;

namespace SPTKnowledgeService.Services
{
    public interface ISubjectService
    {
        Task<SubjectDTO> GetSubjectByCodeAsync(string SubjectCode);
        Task<bool> SubjectExistsAsync(string subjectCode);
    }
}
