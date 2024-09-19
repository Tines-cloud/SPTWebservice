using SPTSubjectService.DTO;

namespace SPTSubjectService.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetSubjectsAsync();
        Task<SubjectDTO> GetSubjectBySubjectCodeAsync(string subjectCode);
        Task AddSubjectAsync(SubjectDTO subjectDTO);
        Task UpdateSubjectAsync(SubjectDTO subjectDTO);
        Task DeleteSubjectAsync(string subjectCode);
        Task<bool> SubjectExistsAsync(string subjectCode);
    }
}
