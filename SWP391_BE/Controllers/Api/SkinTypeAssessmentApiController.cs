using Microsoft.AspNetCore.Mvc;
using Service.SkinType;
using SWP391_BE.Dtos.SkinType;

namespace SWP391_BE.Controllers.Api
{
    [ApiController]
    [Route("api/skintype")]
    public class SkinTypeAssessmentApiController : ControllerBase
    {
        private readonly ISkinTypeAssessmentService _skinTypeService;

        public SkinTypeAssessmentApiController(ISkinTypeAssessmentService skinTypeService)
        {
            _skinTypeService = skinTypeService;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetAssessmentQuestions()
        {
            try
            {
                var questions = await _skinTypeService.GetAssessmentQuestionsAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("assess")]
        public async Task<IActionResult> AssessSkinType([FromBody] SkinAssessmentDto assessment)
        {
            try
            {
                var result = await _skinTypeService.DetermineSkinTypeAsync(assessment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
} 