using Microsoft.AspNetCore.Mvc;
using SWP391_BE.Services;
using SWP391_BE.Dtos;

namespace SWP391_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("product/{productId}")]
        public async Task<IActionResult> AddReview(int productId, [FromBody] ReviewCreateDto review)
        {
            // Add product review
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductReviews(int productId)
        {
            // Get product reviews
        }
    }
} 