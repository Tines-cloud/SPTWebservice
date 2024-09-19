using Microsoft.EntityFrameworkCore;
using SPTKnowledgeService.Data;
using SPTKnowledgeService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPTKnowledgeService.Repositories.Impl
{
    public class StudySessionRepository : IStudySessionRepository
    {
        private readonly ApplicationDbContext _context;

        public StudySessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudySession>> GetStudySessionsAsync()
        {
            return await _context.StudySession
                .Include(ss => ss.Break)
                .ThenInclude(b => b.BreakDurations)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudySession>> GetStudySessionByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.StudySession
                .Include(ss => ss.Break)
                .ThenInclude(b => b.BreakDurations)
                .AsQueryable();

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => u.Username == username);
            }

            if (!string.IsNullOrEmpty(subjectCode))
            {
                query = query.Where(u => u.SubjectCode == subjectCode);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(u => u.Date >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(u => u.Date <= toDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<StudySession> GetStudySessionByIdAsync(int id)
        {
            return await _context.StudySession
                .Include(ss => ss.Break)
                .ThenInclude(b => b.BreakDurations)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddStudySessionAsync(StudySession studySession)
        {
            _context.StudySession.Add(studySession);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudySessionAsync(StudySession studySession)
        {
            var existingStudySession = await _context.StudySession
                .Include(ss => ss.Break)
                .FirstOrDefaultAsync(u => u.Id == studySession.Id);

            if (existingStudySession != null)
            {
                existingStudySession.SubjectCode = studySession.SubjectCode;
                existingStudySession.Username = studySession.Username;
                existingStudySession.Date = studySession.Date;
                existingStudySession.StartTime = studySession.StartTime;
                existingStudySession.EndTime = studySession.EndTime;

                // Update Break details if provided
                if (studySession.Break != null)
                {
                    existingStudySession.Break = studySession.Break;
                }

                _context.Entry(existingStudySession).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteStudySessionAsync(int id)
        {
            var studySession = await _context.StudySession
                .Include(ss => ss.Break)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (studySession != null)
            {
                _context.StudySession.Remove(studySession);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> StudySessionExistsAsync(int id)
        {
            return await _context.StudySession.AnyAsync(e => e.Id == id);
        }
    }
}
