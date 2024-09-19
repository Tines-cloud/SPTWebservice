using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Mappers;
using SPTKnowledgeService.Models;
using SPTKnowledgeService.Repositories;

namespace SPTKnowledgeService.Services.Impl
{
    public class StudySessionService : IStudySessionService
    {
        private readonly IStudySessionRepository _studySessionRepository;
        private readonly KnowledgeMapper _knowledgeMapper;
        private readonly IUserService _userService;
        private readonly ISubjectService _subjectService;
        private readonly ILogger<StudySessionService> _logger;

        public StudySessionService(IStudySessionRepository StudySessionRepository, ILogger<StudySessionService> logger, KnowledgeMapper knowledgeMapper, IUserService userService, ISubjectService subjectService)
        {
            _studySessionRepository = StudySessionRepository;
            _knowledgeMapper = knowledgeMapper;
            _logger = logger;
            _userService = userService;
            _subjectService = subjectService;
        }

        public async Task<IEnumerable<StudySessionDTO>> GetStudySessionsAsync()
        {
            try
            {
                var StudySessions = await _studySessionRepository.GetStudySessionsAsync();
                var StudySessionDTOs = new List<StudySessionDTO>();

                foreach (var StudySession in StudySessions)
                {
                    var dto = await _knowledgeMapper.MapStudySessionToStudySessionDtoAsync(StudySession);
                    StudySessionDTOs.Add(dto);
                }

                return StudySessionDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting StudySessions.");
                throw new ApplicationException("An error occurred while getting StudySessions.", ex);
            }
        }

        public async Task<IEnumerable<StudySessionDTO>> GetStudySessionByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate)
        {
            {
                try
                {
                    await CheckUsernameAndSubject(username, subjectCode);
                    var studySessions = await _studySessionRepository.GetStudySessionByCriteriaAsync(subjectCode, username, fromDate, toDate);
                    var studySessionDTOs = new List<StudySessionDTO>();

                    foreach (var studySession in studySessions)
                    {
                        var dto = await _knowledgeMapper.MapStudySessionToStudySessionDtoAsync(studySession);
                        studySessionDTOs.Add(dto);
                    }

                    return studySessionDTOs;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while getting the StudySession with the given criteria.");
                    throw new ApplicationException($"Unable to get the StudySession with the given criteria.", ex);
                }
            }

        }

        public async Task<StudySessionDTO> GetStudySessionByIdAsync(int id)
        {
            try
            {
                var StudySession = await _studySessionRepository.GetStudySessionByIdAsync(id);
                return await _knowledgeMapper.MapStudySessionToStudySessionDtoAsync(StudySession);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the StudySession with id {id}.");
                throw new ApplicationException($"Unable to get the StudySession with id {id}.", ex);
            }
        }

        public async Task AddStudySessionAsync(StudySessionDTO StudySessionDto)
        {
            try
            {
                await CheckUsernameAndSubject(StudySessionDto.Username, StudySessionDto.Subject.SubjectCode);
                StudySession StudySession = _knowledgeMapper.MapStudySessionDtoToStudySession(StudySessionDto);
                await _studySessionRepository.AddStudySessionAsync(StudySession);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new StudySession.");
                throw new ApplicationException("Unable to add the new StudySession.", ex);
            }
        }

        public async Task UpdateStudySessionAsync(StudySessionDTO StudySessionDto)
        {
            try
            {
                await CheckUsernameAndSubject(StudySessionDto.Username, StudySessionDto.Subject.SubjectCode);
                StudySession StudySession = _knowledgeMapper.MapStudySessionDtoToStudySession(StudySessionDto);
                await _studySessionRepository.UpdateStudySessionAsync(StudySession);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the StudySession with id {StudySessionDto.Id}.");
                throw new ApplicationException($"Unable to update the StudySession with id {StudySessionDto.Id}.", ex);
            }
        }

        public async Task DeleteStudySessionAsync(int id)
        {
            try
            {
                await _studySessionRepository.DeleteStudySessionAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the StudySession with id {id}.");
                throw new ApplicationException($"Unable to delete the StudySession with id {id}.", ex);
            }
        }

        public async Task<bool> StudySessionExistsAsync(int id)
        {
            try
            {
                return await _studySessionRepository.StudySessionExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if the StudySession with id {id} exists.");
                throw new ApplicationException($"Unable to check if the StudySession with id {id} exists.", ex);
            }
        }

        private async Task CheckUsernameAndSubject(string username, string subjectCode)
        {
            if (username == null || subjectCode == null)
            {
                throw new ArgumentNullException("Username or subjectCode is missing");
            }
            if (!await _subjectService.SubjectExistsAsync(subjectCode))
            {
                _logger.LogError($"Subject not found");
                throw new ApplicationException("Subject not found");
            }
            if (!await _userService.UserExistsAsync(username))
            {
                _logger.LogError($"User not found");
                throw new ApplicationException("User not found");
            }
        }
    }
}
