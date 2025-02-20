using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service;
using SWP391_BE.DTOs;
using Data.Models;
using Microsoft.Extensions.Logging;

namespace SWP391_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService productService, 
            IMapper mapper,
            ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO createProductDTO)
        {
            try
            {
                // Validate required relationships
                if (!await ValidateRelationships(createProductDTO))
                {
                    return BadRequest("Invalid Brand, Volume, SkinType or Category ID");
                }

                var product = _mapper.Map<Product>(createProductDTO);
                product.CreatedAt = DateTime.UtcNow;

                await _productService.AddProductAsync(product);

                // Add images
                foreach (var imageUrl in createProductDTO.ImageUrls)
                {
                    product.Images.Add(new ProductImage { ImageUrl = imageUrl });
                }

                await _productService.UpdateProductAsync(product);

                return CreatedAtAction(
                    nameof(GetProduct), 
                    new { id = product.ProductId }, 
                    _mapper.Map<ProductDTO>(product)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<bool> ValidateRelationships(CreateProductDTO dto)
        {
            // Validate Brand exists
            if (dto.BrandId.HasValue)
            {
                var brand = await _brandService.GetByIdAsync(dto.BrandId.Value);
                if (brand == null) return false;
            }

            // Validate Volume exists
            if (dto.VolumeId.HasValue)
            {
                var volume = await _volumeService.GetByIdAsync(dto.VolumeId.Value);
                if (volume == null) return false;
            }

            // Validate SkinType exists
            if (dto.SkinTypeId.HasValue)
            {
                var skinType = await _skinTypeService.GetByIdAsync(dto.SkinTypeId.Value);
                if (skinType == null) return false;
            }

            // Validate Category exists
            if (dto.CategoryId.HasValue)
            {
                var category = await _categoryService.GetByIdAsync(dto.CategoryId.Value);
                if (category == null) return false;
            }

            return true;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
        {
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _mapper.Map(updateProductDTO, existingProduct);
            await _productService.UpdateProductAsync(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
} 