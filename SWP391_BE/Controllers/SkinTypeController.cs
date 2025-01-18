using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SWP391_BE.Services;
using SWP391_BE.Dtos;

namespace SWP391_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkinTypeController : ControllerBase
    {
        private readonly ISkintypeService _skintypeService;

        public SkinTypeController(ISkintypeService skintypeService)
        {
            _skintypeService = skintypeService;
        }

        [HttpPost("assessment")]
        public async Task<IActionResult> SubmitAssessment([FromBody] SkinAssessmentDto answers)
        {
            // Implement skin type assessment logic
        }

        [HttpGet("routine/{skinTypeId}")]
        public async Task<IActionResult> GetSkinRoutine(int skinTypeId)
        {
            // Get recommended routine for skin type
        }
    }
} 