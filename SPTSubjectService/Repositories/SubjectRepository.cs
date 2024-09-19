using SPTSubjectService.Models;

namespace SPTSubjectService.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetSubjectsAsync();
        Task<Subject> GetSubjectBySubjectCodeAsync(string subjectCode);
        Task AddSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(string subjectCode);
        Task<bool> SubjectExistsAsync(string subjectCode);
    }
}
