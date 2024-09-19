using Microsoft.AspNetCore.Mvc;
using SPTSubjectService.DTO;
using SPTSubjectService.Services;

namespace SPTSubjectService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetSubjects()
        {
            try
            {
                var subjects = await _subjectService.GetSubjectsAsync();
                return Ok(subjects);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{subjectCode}")]
        public async Task<ActionResult<SubjectDTO>> GetSubject(string subjectCode)
        {
            try
            {
                var subject = await _subjectService.GetSubjectBySubjectCodeAsync(subjectCode);

                if (subject == null)
                {
                    return NotFound($"Subject with subjectcode {subjectCode} not found.");
                }

                return Ok(subject);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SubjectDTO>> PostSubject(SubjectDTO subjectDTO)
        {
            if (subjectDTO == null)
            {
                return BadRequest("subject data is null.");
            }

            if (await _subjectService.SubjectExistsAsync(subjectDTO.SubjectCode))
            {
                return Conflict($"Subject with subjectCode {subjectDTO.SubjectCode} already exists.");
            }

            try
            {
                await _subjectService.AddSubjectAsync(subjectDTO);
                return CreatedAtAction(nameof(GetSubject), new { subjectCode = subjectDTO.SubjectCode }, subjectDTO);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{subjectCode}")]
        public async Task<IActionResult> PutSubject(string subjectCode, SubjectDTO subjectDto)
        {
            if (subjectCode != subjectDto.SubjectCode)
            {
                return BadRequest("SubjectCode mismatch.");
            }

            if (!await _subjectService.SubjectExistsAsync(subjectCode))
            {
                return NotFound($"Subject with subjectCode {subjectCode} not found.");
            }

            try
            {
                await _subjectService.UpdateSubjectAsync(subjectDto);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{subjectCode}")]
        public async Task<IActionResult> DeleteSubject(string subjectCode)
        {
            if (!await _subjectService.SubjectExistsAsync(subjectCode))
            {
                return NotFound($"Subject with subjectCode {subjectCode} not found.");
            }

            try
            {
                await _subjectService.DeleteSubjectAsync(subjectCode);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Exists/{subjectCode}")]
        public async Task<IActionResult> SubjectExists(string subjectCode)
        {
            try
            {
                var exists = await _subjectService.SubjectExistsAsync(subjectCode);
                return Ok(exists);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
