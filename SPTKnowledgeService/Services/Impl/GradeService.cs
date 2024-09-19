using SPTGradeService.Repositories;
using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Mappers;
using SPTKnowledgeService.Models;

namespace SPTGradeService.Services.Impl
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly KnowledgeMapper _knowledgeMapper;
        private readonly ILogger<GradeService> _logger;

        public GradeService(IGradeRepository GradeRepository, ILogger<GradeService> logger, KnowledgeMapper knowledgeMapper)
        {
            _gradeRepository = GradeRepository;
            _knowledgeMapper = knowledgeMapper;
            _logger = logger;
        }

        public async Task<IEnumerable<GradeDTO>> GetGradesAsync()
        {
            try
            {
                var Grades = await _gradeRepository.GetGradesAsync();
                return Grades.Select(_knowledgeMapper.MapGradeToGradeDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting Grades.");
                throw new ApplicationException("Unable to get Grades.", ex);
            }
        }

        public async Task<GradeDTO> GetGradeByCodeAsync(string code)
        {
            try
            {
                var grade = await _gradeRepository.GetGradeByCodeAsync(code);
                return _knowledgeMapper.MapGradeToGradeDto(grade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the Grade with code {code}.");
                throw new ApplicationException($"Unable to get the Grade with code {code}.", ex);
            }
        }

        public async Task AddGradeAsync(GradeDTO GradeDto)
        {
            try
            {
                Grade grade = _knowledgeMapper.MapGradeDtoToGrade(GradeDto);
                await _gradeRepository.AddGradeAsync(grade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new Grade.");
                throw new ApplicationException("Unable to add the new Grade.", ex);
            }
        }

        public async Task UpdateGradeAsync(GradeDTO GradeDto)
        {
            try
            {
                Grade Grade = _knowledgeMapper.MapGradeDtoToGrade(GradeDto);
                await _gradeRepository.UpdateGradeAsync(Grade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the Grade with code {GradeDto.GradeCode}.");
                throw new ApplicationException($"Unable to update the Grade with code {GradeDto.GradeCode}.", ex);
            }
        }

        public async Task DeleteGradeAsync(string code)
        {
            try
            {
                await _gradeRepository.DeleteGradeAsync(code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the Grade with code {code}.");
                throw new ApplicationException($"Unable to delete the Grade with code {code}.", ex);
            }
        }

        public async Task<bool> GradeExistsAsync(string code)
        {
            try
            {
                return await _gradeRepository.GradeExistsAsync(code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if the Grade with code {code} exists.");
                throw new ApplicationException($"Unable to check if the Grade with code {code} exists.", ex);
            }
        }
    }
}
