using Microsoft.AspNetCore.Mvc;
using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Services;
using SPTKnowledgeService.Services.Impl;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPTKnowledgeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreakController : ControllerBase
    {
        private readonly IBreakService _breakService;

        public BreakController(IBreakService breakService)
        {
            _breakService = breakService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreakDTO>>> GetBreaks()
        {
            try
            {
                var breaks = await _breakService.GetBreaksAsync();
                return Ok(breaks);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BreakDTO>> GetBreakById(int id)
        {
            try
            {
                var breakDto = await _breakService.GetBreakByIdAsync(id);

                if (breakDto == null)
                {
                    return NotFound($"Break with id {id} not found.");
                }

                return Ok(breakDto);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search/{username}")]
        public async Task<ActionResult<IEnumerable<BreakDTO>>> GetBreak(string username, [FromQuery] string subjectCode = null, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
        {
            try
            {
                var breakDTOs = await _breakService.GetBreakByCriteriaAsync(subjectCode, username, fromDate, toDate);

                if (breakDTOs == null || !breakDTOs.Any())
                {
                    return NotFound($"Break with the given criteria not found.");
                }
                return Ok(breakDTOs);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BreakDTO>> PostBreak(BreakDTO breakDTO)
        {
            if (breakDTO == null)
            {
                return BadRequest("Break data is null.");
            }

            try
            {
                await _breakService.AddBreakAsync(breakDTO);
                return CreatedAtAction(nameof(GetBreakById), new { id = breakDTO.Id }, breakDTO);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreak(int id, BreakDTO breakDto)
        {
            if (id != breakDto.Id)
            {
                return BadRequest("Id mismatch.");
            }

            if (!await _breakService.BreakExistsAsync(id))
            {
                return NotFound($"Break with Id {id} not found.");
            }

            try
            {
                await _breakService.UpdateBreakAsync(breakDto);
                return Ok(breakDto);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreak(int id)
        {
            if (!await _breakService.BreakExistsAsync(id))
            {
                return NotFound($"Break with Id {id} not found.");
            }

            try
            {
                await _breakService.DeleteBreakAsync(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
