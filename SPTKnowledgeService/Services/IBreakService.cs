using SPTKnowledgeService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPTKnowledgeService.Services
{
    public interface IBreakService
    {
        Task<IEnumerable<BreakDTO>> GetBreaksAsync();
        Task<IEnumerable<BreakDTO>> GetBreakByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate);
        Task<BreakDTO> GetBreakByIdAsync(int id);
        Task AddBreakAsync(BreakDTO breakDto);
        Task UpdateBreakAsync(BreakDTO breakDto);
        Task DeleteBreakAsync(int id);
        Task<bool> BreakExistsAsync(int id);
    }
}
