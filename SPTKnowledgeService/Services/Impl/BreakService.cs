using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Mappers;
using SPTKnowledgeService.Models;
using SPTKnowledgeService.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPTKnowledgeService.Services.Impl
{
    public class BreakService : IBreakService
    {
        private readonly IBreakRepository _breakRepository;
        private readonly KnowledgeMapper _knowledgeMapper;
        private readonly ILogger<BreakService> _logger;

        public BreakService(IBreakRepository breakRepository, KnowledgeMapper knowledgeMapper, ILogger<BreakService> logger)
        {
            _breakRepository = breakRepository;
            _knowledgeMapper = knowledgeMapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BreakDTO>> GetBreaksAsync()
        {
            try
            {
                var breaks = await _breakRepository.GetBreaksAsync();
                var breakDTOs = new List<BreakDTO>();

                foreach (var breakEntity in breaks)
                {
                    var dto = await _knowledgeMapper.MapBreakToBreakDtoAsync(breakEntity);
                    breakDTOs.Add(dto);
                }

                return breakDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting breaks.");
                throw new ApplicationException("An error occurred while getting breaks.", ex);
            }
        }

        public async Task<BreakDTO> GetBreakByIdAsync(int id)
        {
            try
            {
                var breakEntity = await _breakRepository.GetBreakByIdAsync(id);
                return await _knowledgeMapper.MapBreakToBreakDtoAsync(breakEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the break with id {id}.");
                throw new ApplicationException($"Unable to get the break with id {id}.", ex);
            }
        }

        public async Task AddBreakAsync(BreakDTO breakDto)
        {
            try
            {
                var breakEntity = _knowledgeMapper.MapBreakDtoToBreak(breakDto);
                await _breakRepository.AddBreakAsync(breakEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new break.");
                throw new ApplicationException("Unable to add the new break.", ex);
            }
        }

        public async Task UpdateBreakAsync(BreakDTO breakDto)
        {
            try
            {
                var breakEntity = _knowledgeMapper.MapBreakDtoToBreak(breakDto);
                await _breakRepository.UpdateBreakAsync(breakEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the break with id {breakDto.Id}.");
                throw new ApplicationException($"Unable to update the break with id {breakDto.Id}.", ex);
            }
        }

        public async Task DeleteBreakAsync(int id)
        {
            try
            {
                await _breakRepository.DeleteBreakAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the break with id {id}.");
                throw new ApplicationException($"Unable to delete the break with id {id}.", ex);
            }
        }

        public async Task<bool> BreakExistsAsync(int id)
        {
            try
            {
                return await _breakRepository.BreakExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if the break with id {id} exists.");
                throw new ApplicationException($"Unable to check if the break with id {id} exists.", ex);
            }
        }

        public async Task<IEnumerable<BreakDTO>> GetBreakByCriteriaAsync(string subjectCode, string username, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var breaks = await _breakRepository.GetBreakByCriteriaAsync(subjectCode, username, fromDate, toDate);
                var breakDTOs = new List<BreakDTO>();

                foreach (var breakEntity in breaks)
                {
                    var dto = await _knowledgeMapper.MapBreakToBreakDtoAsync(breakEntity);
                    breakDTOs.Add(dto);
                }

                return breakDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting breaks by criteria.");
                throw new ApplicationException("Unable to get breaks by criteria.", ex);
            }
        }
    }
}
