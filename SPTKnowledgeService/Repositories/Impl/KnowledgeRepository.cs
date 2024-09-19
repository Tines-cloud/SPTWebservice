using Microsoft.EntityFrameworkCore;
using SPTKnowledgeService.Data;
using SPTKnowledgeService.Models;

namespace SPTKnowledgeService.Repositories.Impl
{
    public class KnowledgeRepository : IKnowledgeRepository
    {
        private readonly ApplicationDbContext _context;

        public KnowledgeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Knowledge>> GetKnowledgesAsync()
        {
            return await _context.Knowledge
                .ToListAsync();
        }

        public async Task<IEnumerable<Knowledge>> GetKnowledgeByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Knowledge.AsQueryable();

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


        public async Task<Knowledge> GetLatestKnowledgeAsync(string subjectCode, string username)
        {
            return await _context.Knowledge
                .Where(u => u.Username == username && u.SubjectCode == subjectCode)
                .OrderByDescending(u => u.Date)
                .FirstOrDefaultAsync();
        }


        public async Task<Knowledge> GetKnowledgeByIdAsync(int id)
        {
            return await _context.Knowledge
                .FirstOrDefaultAsync(predicate: u => u.Id == id);
        }

        public async Task AddKnowledgeAsync(Knowledge Knowledge)
        {
            _context.Knowledge.Add(Knowledge);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKnowledgeAsync(Knowledge Knowledge)
        {
            var existingKnowledge = await _context.Knowledge
        .FirstOrDefaultAsync(u => u.Id == Knowledge.Id);

            if (existingKnowledge != null)
            {
                existingKnowledge.SubjectCode = Knowledge.SubjectCode;
                existingKnowledge.Username = Knowledge.Username;
                existingKnowledge.Date = Knowledge.Date;
                existingKnowledge.GradeCode = Knowledge.GradeCode;

                _context.Entry(existingKnowledge).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteKnowledgeAsync(int id)
        {
            var Knowledge = await _context.Knowledge
                .FirstOrDefaultAsync(u => u.Id == id);

            if (Knowledge != null)
            {
                _context.Knowledge.Remove(Knowledge);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> KnowledgeExistsAsync(int id)
        {
            return await _context.Knowledge.AnyAsync(e => e.Id == id);
        }
    }
}
