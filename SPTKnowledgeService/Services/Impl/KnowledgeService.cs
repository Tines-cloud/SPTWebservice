using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Mappers;
using SPTKnowledgeService.Models;
using SPTKnowledgeService.Repositories;

namespace SPTKnowledgeService.Services.Impl
{
    public class KnowledgeService : IKnowledgeService
    {
        private readonly IKnowledgeRepository _knowledgeRepository;
        private readonly KnowledgeMapper _knowledgeMapper;
        private readonly IUserService _userService;
        private readonly ISubjectService _subjectService;
        private readonly ILogger<KnowledgeService> _logger;

        public KnowledgeService(IKnowledgeRepository KnowledgeRepository, ILogger<KnowledgeService> logger, KnowledgeMapper knowledgeMapper, IUserService userService, ISubjectService subjectService)
        {
            _knowledgeRepository = KnowledgeRepository;
            _knowledgeMapper = knowledgeMapper;
            _logger = logger;
            _userService = userService;
            _subjectService = subjectService;
        }

        public async Task<IEnumerable<KnowledgeDTO>> GetKnowledgesAsync()
        {
            try
            {
                var knowledges = await _knowledgeRepository.GetKnowledgesAsync();
                var knowledgeDTOs = new List<KnowledgeDTO>();

                foreach (var knowledge in knowledges)
                {
                    var dto = await _knowledgeMapper.MapKnowledgeToKnowledgeDtoAsync(knowledge);
                    knowledgeDTOs.Add(dto);
                }

                return knowledgeDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting knowledges.");
                throw new ApplicationException("An error occurred while getting knowledges.", ex);
            }
        }

        public async Task<IEnumerable<KnowledgeDTO>> GetKnowledgeByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                await CheckUsernameAndSubject(username, subjectCode);
                var knowledges = await _knowledgeRepository.GetKnowledgeByCriteriaAsync(subjectCode, username, fromDate, toDate);
                var knowledgeDTOs = new List<KnowledgeDTO>();

                foreach (var knowledge in knowledges)
                {
                    var dto = await _knowledgeMapper.MapKnowledgeToKnowledgeDtoAsync(knowledge);
                    knowledgeDTOs.Add(dto);
                }

                return knowledgeDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the knowledge with the given criteria.");
                throw new ApplicationException($"Unable to get the knowledge with the given criteria.", ex);
            }
        }

        public async Task<KnowledgeDTO> GetLatestKnowledgeAsync(string subjectCode, string username)
        {
            try
            {
                await CheckUsernameAndSubject(username, subjectCode);
                var knowledge = await _knowledgeRepository.GetLatestKnowledgeAsync(subjectCode, username);
                return await _knowledgeMapper.MapKnowledgeToKnowledgeDtoAsync(knowledge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the knowledge with subjectCode {subjectCode} and username {username}.");
                throw new ApplicationException($"Unable to get the knowledge with subjectCode {subjectCode} and username {username}.", ex);
            }
        }

        public async Task<KnowledgeDTO> GetKnowledgeByIdAsync(int id)
        {
            try
            {
                var knowledge = await _knowledgeRepository.GetKnowledgeByIdAsync(id);
                return await _knowledgeMapper.MapKnowledgeToKnowledgeDtoAsync(knowledge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the knowledge with id {id}.");
                throw new ApplicationException($"Unable to get the knowledge with id {id}.", ex);
            }
        }

        public async Task AddKnowledgeAsync(KnowledgeDTO knowledgeDto)
        {
            try
            {
                await CheckUsernameAndSubject(knowledgeDto.Username, knowledgeDto.Subject.SubjectCode);
                Knowledge knowledge = _knowledgeMapper.MapKnowledgeDtoToKnowledge(knowledgeDto);
                await _knowledgeRepository.AddKnowledgeAsync(knowledge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new knowledge.");
                throw new ApplicationException("Unable to add the new knowledge.", ex);
            }
        }

        public async Task UpdateKnowledgeAsync(KnowledgeDTO knowledgeDto)
        {
            try
            {
                await CheckUsernameAndSubject(knowledgeDto.Username, knowledgeDto.Subject.SubjectCode);
                Knowledge knowledge = _knowledgeMapper.MapKnowledgeDtoToKnowledge(knowledgeDto);
                await _knowledgeRepository.UpdateKnowledgeAsync(knowledge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the knowledge with id {knowledgeDto.Id}.");
                throw new ApplicationException($"Unable to update the knowledge with id {knowledgeDto.Id}.", ex);
            }
        }

        public async Task DeleteKnowledgeAsync(int id)
        {
            try
            {
                await _knowledgeRepository.DeleteKnowledgeAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the knowledge with id {id}.");
                throw new ApplicationException($"Unable to delete the knowledge with id {id}.", ex);
            }
        }

        public async Task<bool> KnowledgeExistsAsync(int id)
        {
            try
            {
                return await _knowledgeRepository.KnowledgeExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if the knowledge with id {id} exists.");
                throw new ApplicationException($"Unable to check if the knowledge with id {id} exists.", ex);
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
