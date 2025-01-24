using Microsoft.AspNetCore.Mvc;
using Service.Product;
using SWP391_BE.Dtos.Product;

namespace SWP391_BE.Controllers.Api
{
    [ApiController]
    [Route("api/products")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilterDto filter)
        {
            var products = await _productService.GetProductsAsync(filter);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("compare")]
        public async Task<IActionResult> CompareProducts([FromQuery] List<int> productIds)
        {
            var comparison = await _productService.CompareProductsAsync(productIds);
            return Ok(comparison);
        }

        [HttpGet("recommended/{skinTypeId}")]
        public async Task<IActionResult> GetRecommendedProducts(int skinTypeId)
        {
            var products = await _productService.GetRecommendedProductsAsync(skinTypeId);
            return Ok(products);
        }
    }
} 