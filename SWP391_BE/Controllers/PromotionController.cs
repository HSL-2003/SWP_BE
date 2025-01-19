using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SWP391_BE.Services;

namespace SWP391_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActivePromotions()
        {
            // Get active promotions
        }

        [HttpGet("points/{userId}")]
        public async Task<IActionResult> GetUserPoints(int userId)
        {
            // Get user loyalty points
        }
    }
} 