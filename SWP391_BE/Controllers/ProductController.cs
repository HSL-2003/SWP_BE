using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391_BE.Services;
using SWP391_BE.Dtos;

namespace SWP391_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductFilterDto filter)
        {
            // Get products with filtering
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            // Get single product
        }

        [HttpGet("compare")]
        public async Task<IActionResult> CompareProducts([FromQuery] List<int> productIds)
        {
            // Compare products
        }

        [HttpGet("recommended/{skinTypeId}")]
        public async Task<IActionResult> GetRecommendedProducts(int skinTypeId)
        {
            // Get recommended products for skin type
        }
    }
} 