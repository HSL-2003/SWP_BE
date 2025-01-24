using Microsoft.AspNetCore.Mvc;
using Service.SkinType;
using SWP391_BE.Dtos.SkinType;

namespace SWP391_BE.Controllers.Api
{
    [ApiController]
    [Route("api/skintypes")]
    public class SkinTypeApiController : ControllerBase
    {
        private readonly ISkinTypeService _skinTypeService;

        public SkinTypeApiController(ISkinTypeService skinTypeService)
        {
            _skinTypeService = skinTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinTypes()
        {
            var skinTypes = await _skinTypeService.GetAllSkinTypesAsync();
            return Ok(skinTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkinType(int id)
        {
            var skinType = await _skinTypeService.GetSkinTypeByIdAsync(id);
            if (skinType == null)
                return NotFound();
            return Ok(skinType);
        }

        [HttpPost("assess")]
        public async Task<IActionResult> AssessSkinType([FromBody] SkinAssessmentDto assessment)
        {
            var result = await _skinTypeService.DetermineSkinTypeAsync(assessment);
            return Ok(result);
        }

        [HttpGet("{id}/routine")]
        public async Task<IActionResult> GetSkinRoutine(int id)
        {
            var routine = await _skinTypeService.GetSkinRoutineAsync(id);
            return Ok(routine);
        }

        [HttpGet("{id}/recommended-products")]
        public async Task<IActionResult> GetRecommendedProducts(int id)
        {
            var products = await _skinTypeService.GetRecommendedProductsForSkinTypeAsync(id);
            return Ok(products);
        }
    }
} 