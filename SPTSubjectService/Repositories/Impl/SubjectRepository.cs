using Microsoft.EntityFrameworkCore;
using SPTSubjectService.Data;
using SPTSubjectService.Models;

namespace SPTSubjectService.Repositories.Impl
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            return await _context.Subject
                .ToListAsync();
        }

        public async Task<Subject> GetSubjectBySubjectCodeAsync(string subjectCode)
        {
            return await _context.Subject
                .FirstOrDefaultAsync(u => u.SubjectCode == subjectCode);
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subject.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            var existingSubject = await _context.Subject
        .FirstOrDefaultAsync(u => u.SubjectCode == subject.SubjectCode);

            if (existingSubject != null)
            {
                existingSubject.SubjectName = subject.SubjectName;
                existingSubject.Modules = subject.Modules;

                _context.Entry(existingSubject).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSubjectAsync(string subjectCode)
        {
            var subject = await _context.Subject
                .FirstOrDefaultAsync(u => u.SubjectCode == subjectCode);

            if (subject != null)
            {
                _context.Subject.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SubjectExistsAsync(string subjectCode)
        {
            return await _context.Subject.AnyAsync(e => e.SubjectCode == subjectCode);
        }
    }
}
