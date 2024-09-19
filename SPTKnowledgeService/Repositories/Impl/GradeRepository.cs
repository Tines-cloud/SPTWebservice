using Microsoft.EntityFrameworkCore;
using SPTKnowledgeService.Data;
using SPTKnowledgeService.Models;
namespace SPTGradeService.Repositories.Impl
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext _context;

        public GradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grade>> GetGradesAsync()
        {
            return await _context.Grade
                .ToListAsync();
        }

        public async Task<Grade> GetGradeByCodeAsync(string code)
        {
            return await _context.Grade
                .FirstOrDefaultAsync(u => u.code == code);
        }

        public async Task AddGradeAsync(Grade Grade)
        {
            _context.Grade.Add(Grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            var existingGrade = await _context.Grade
        .FirstOrDefaultAsync(u => u.code == grade.code);

            if (existingGrade != null)
            {
                existingGrade.name = grade.name;

                _context.Entry(existingGrade).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGradeAsync(string code)
        {
            var Grade = await _context.Grade
                .FirstOrDefaultAsync(u => u.code == code);

            if (Grade != null)
            {
                _context.Grade.Remove(Grade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> GradeExistsAsync(string code)
        {
            return await _context.Grade.AnyAsync(e => e.code == code);
        }
    }
}
