using SPTKnowledgeService.DTO;

namespace SPTGradeService.Services
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDTO>> GetGradesAsync();
        Task<GradeDTO> GetGradeByCodeAsync(string code);
        Task AddGradeAsync(GradeDTO GradeDTO);
        Task UpdateGradeAsync(GradeDTO GradeDTO);
        Task DeleteGradeAsync(string code);
        Task<bool> GradeExistsAsync(string code);
    }
}
