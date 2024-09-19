using Microsoft.EntityFrameworkCore;
using SPTKnowledgeService.Data;
using SPTKnowledgeService.Models;

namespace SPTKnowledgeService.Repositories.Impl
{
    public class BreakRepository : IBreakRepository
    {
        private readonly ApplicationDbContext _context;

        public BreakRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Break>> GetBreaksAsync()
        {
            return await _context.Breaks
                .Include(b => b.BreakDurations)
                .ToListAsync();
        }

        public async Task<Break> GetBreakByIdAsync(int id)
        {
            return await _context.Breaks
                .Include(b => b.BreakDurations)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBreakAsync(Break breakEntity)
        {
            _context.Breaks.Add(breakEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBreakAsync(Break breakEntity)
        {
            _context.Breaks.Update(breakEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBreakAsync(int id)
        {
            var breakEntity = await GetBreakByIdAsync(id);
            _context.Breaks.Remove(breakEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BreakExistsAsync(int id)
        {
            return await _context.Breaks.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Break>> GetBreakByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Breaks.Include(b => b.BreakDurations).AsQueryable();

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

    }
}
