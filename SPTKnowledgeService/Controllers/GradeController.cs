using Microsoft.AspNetCore.Mvc;
using SPTGradeService.Services;
using SPTKnowledgeService.DTO;

namespace SPTGradeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService GradeService)
        {
            _gradeService = GradeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDTO>>> GetGrades()
        {
            try
            {
                var Grades = await _gradeService.GetGradesAsync();
                return Ok(Grades);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<GradeDTO>> GetGrade(string code)
        {
            try
            {
                var Grade = await _gradeService.GetGradeByCodeAsync(code);

                if (Grade == null)
                {
                    return NotFound($"Grade with code {code} not found.");
                }

                return Ok(Grade);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GradeDTO>> PostGrade(GradeDTO gradeDto)
        {
            if (gradeDto == null)
            {
                return BadRequest("Grade data is null.");
            }

            try
            {
                await _gradeService.AddGradeAsync(gradeDto);
                return CreatedAtAction(nameof(GetGrade), new { code = gradeDto.GradeCode }, gradeDto);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> PutGrade(string code, GradeDTO GradeDto)
        {
            if (code != GradeDto.GradeCode)
            {
                return BadRequest("Id mismatch.");
            }

            if (!await _gradeService.GradeExistsAsync(code))
            {
                return NotFound($"Grade with code {code} not found.");
            }

            try
            {
                await _gradeService.UpdateGradeAsync(GradeDto);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteGrade(string code)
        {
            if (!await _gradeService.GradeExistsAsync(code))
            {
                return NotFound($"Grade with code {code} not found.");
            }

            try
            {
                await _gradeService.DeleteGradeAsync(code);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
