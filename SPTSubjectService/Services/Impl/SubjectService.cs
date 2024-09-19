using SPTSubjectService.DTO;
using SPTSubjectService.Mappers;
using SPTSubjectService.Models;
using SPTSubjectService.Repositories;

namespace SPTSubjectService.Services.Impl
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<SubjectService> _logger;

        public SubjectService(ISubjectRepository subjectRepository, ILogger<SubjectService> logger)
        {
            _subjectRepository = subjectRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SubjectDTO>> GetSubjectsAsync()
        {
            try
            {
                var subjects = await _subjectRepository.GetSubjectsAsync();
                return subjects.Select(SubjectMapper.MapSubjectToSubjectDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting subjects.");
                throw new ApplicationException("Unable to get subjects.", ex);
            }
        }

        public async Task<SubjectDTO> GetSubjectBySubjectCodeAsync(string subjectCode)
        {
            try
            {
                var subject = await _subjectRepository.GetSubjectBySubjectCodeAsync(subjectCode);
                return SubjectMapper.MapSubjectToSubjectDto(subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the subject with subjectCode {subjectCode}.");
                throw new ApplicationException($"Unable to get the subject with subjectCode {subjectCode}.", ex);
            }
        }

        public async Task AddSubjectAsync(SubjectDTO subjectDto)
        {
            try
            {
                Subject subject = SubjectMapper.MapSubjectDtoToSubject(subjectDto);
                await _subjectRepository.AddSubjectAsync(subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new subject.");
                throw new ApplicationException("Unable to add the new subject.", ex);
            }
        }

        public async Task UpdateSubjectAsync(SubjectDTO subjectDto)
        {
            try
            {
                Subject subject = SubjectMapper.MapSubjectDtoToSubject(subjectDto);
                await _subjectRepository.UpdateSubjectAsync(subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the subject with subjectCode {subjectDto.SubjectCode}.");
                throw new ApplicationException($"Unable to update the subject with subjectCode {subjectDto.SubjectCode}.", ex);
            }
        }

        public async Task DeleteSubjectAsync(string subjectCode)
        {
            try
            {
                await _subjectRepository.DeleteSubjectAsync(subjectCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the subject with subjectCode {subjectCode}.");
                throw new ApplicationException($"Unable to delete the subject with subjectCode {subjectCode}.", ex);
            }
        }

        public async Task<bool> SubjectExistsAsync(string subjectCode)
        {
            try
            {
                return await _subjectRepository.SubjectExistsAsync(subjectCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if the subject with subjectCode {subjectCode} exists.");
                throw new ApplicationException($"Unable to check if the subject with subjectCode {subjectCode} exists.", ex);
            }
        }
    }
}
