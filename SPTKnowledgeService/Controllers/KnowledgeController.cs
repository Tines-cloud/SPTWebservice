using Microsoft.AspNetCore.Mvc;
using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Services;

namespace SPTKnowledgeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnowledgeController : ControllerBase
    {
        private readonly IKnowledgeService _knowledgeService;

        public KnowledgeController(IKnowledgeService knowledgeService)
        {
            _knowledgeService = knowledgeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnowledgeDTO>>> GetKnowledges()
        {
            try
            {
                var knowledges = await _knowledgeService.GetKnowledgesAsync();
                return Ok(knowledges);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KnowledgeDTO>> GetKnowledgeById(int id)
        {
            try
            {
                var knowledge = await _knowledgeService.GetKnowledgeByIdAsync(id);

                if (knowledge == null)
                {
                    return NotFound($"Knowledge with id {id} not found.");
                }

                return Ok(knowledge);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("latest/{username}/{subjectCode}")]
        public async Task<ActionResult<KnowledgeDTO>> GetLatestKnowledge(string subjectCode, string username)
        {
            try
            {
                var knowledge = await _knowledgeService.GetLatestKnowledgeAsync(subjectCode, username);

                if (knowledge == null)
                {
                    return NotFound($"Knowledge with subjectCode {subjectCode} and username {username} not found.");
                }

                return Ok(knowledge);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search/{username}")]
        public async Task<ActionResult<IEnumerable<KnowledgeDTO>>> GetUserKnowledge(string username, [FromQuery] string subjectCode = null, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
        {
            try
            {
                var knowledge = await _knowledgeService.GetKnowledgeByCriteriaAsync(subjectCode, username, fromDate, toDate);

                if (knowledge == null || !knowledge.Any())
                {
                    return NotFound($"Knowledge with the given criteria not found.");
                }
                return Ok(knowledge);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<KnowledgeDTO>> PostKnowledge(KnowledgeDTO knowledgeDTO)
        {
            if (knowledgeDTO == null)
            {
                return BadRequest("Knowledge data is null.");
            }

            try
            {
                await _knowledgeService.AddKnowledgeAsync(knowledgeDTO);
                return Ok(knowledgeDTO);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnowledge(int id, KnowledgeDTO knowledgeDto)
        {
            if (id != knowledgeDto.Id)
            {
                return BadRequest("Id mismatch.");
            }

            if (!await _knowledgeService.KnowledgeExistsAsync(id))
            {
                return NotFound($"Knowledge with Id {id} not found.");
            }

            try
            {
                await _knowledgeService.UpdateKnowledgeAsync(knowledgeDto);
                return Ok(knowledgeDto);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnowledge(int id)
        {
            if (!await _knowledgeService.KnowledgeExistsAsync(id))
            {
                return NotFound($"Knowledge with Id {id} not found.");
            }

            try
            {
                await _knowledgeService.DeleteKnowledgeAsync(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
