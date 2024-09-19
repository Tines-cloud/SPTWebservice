using Microsoft.AspNetCore.Mvc;
using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Services;

namespace SPTStudySessionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudySessionController : ControllerBase
    {
        private readonly IStudySessionService _studySessionService;

        public StudySessionController(IStudySessionService studySessionService)
        {
            _studySessionService = studySessionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudySessionDTO>>> GetStudySessions()
        {
            try
            {
                var StudySessions = await _studySessionService.GetStudySessionsAsync();
                return Ok(StudySessions);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudySessionDTO>> GetStudySessionById(int id)
        {
            try
            {
                var StudySession = await _studySessionService.GetStudySessionByIdAsync(id);

                if (StudySession == null)
                {
                    return NotFound($"StudySession with id {id} not found.");
                }

                return Ok(StudySession);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search/{username}")]
        public async Task<ActionResult<IEnumerable<StudySessionDTO>>> GetUserStudySession(string username, [FromQuery] string subjectCode = null, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
        {
            try
            {
                var studySessions = await _studySessionService.GetStudySessionByCriteriaAsync(subjectCode, username, fromDate, toDate);

                if (studySessions == null || !studySessions.Any())
                {
                    return NotFound($"StudySessions with the given criteria not found.");
                }
                return Ok(studySessions);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<StudySessionDTO>> PostStudySession(StudySessionDTO studySessionDTO)
        {
            if (studySessionDTO == null)
            {
                return BadRequest("StudySession data is null.");
            }

            try
            {
                await _studySessionService.AddStudySessionAsync(studySessionDTO);
                return Ok(studySessionDTO);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudySession(int id, StudySessionDTO studySessionDto)
        {
            if (id != studySessionDto.Id)
            {
                return BadRequest("Id mismatch.");
            }

            if (!await _studySessionService.StudySessionExistsAsync(id))
            {
                return NotFound($"StudySession with Id {id} not found.");
            }

            try
            {
                await _studySessionService.UpdateStudySessionAsync(studySessionDto);
                return Ok(studySessionDto);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudySession(int id)
        {
            if (!await _studySessionService.StudySessionExistsAsync(id))
            {
                return NotFound($"StudySession with Id {id} not found.");
            }

            try
            {
                await _studySessionService.DeleteStudySessionAsync(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
