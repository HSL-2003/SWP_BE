using Data.Models;
using Repo;
using Microsoft.Extensions.Logging;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
                throw;
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {Id} not found", id);
                }
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product with ID {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId)
        {
            try
            {
                return await _productRepository.GetProductsByBrandAsync(brandId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for brand {BrandId}", brandId);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                return await _productRepository.GetProductsByCategoryAsync(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for category {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsBySkinTypeAsync(int skinTypeId)
        {
            try
            {
                return await _productRepository.GetProductsBySkinTypeAsync(skinTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for skin type {SkinTypeId}", skinTypeId);
                throw;
            }
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));

                product.CreatedAt = DateTime.UtcNow;

                // Set first image as main image if there are images
                if (product.Images?.Any() == true)
                {
                    product.Images.First().IsMainImage = true;
                }

                await _productRepository.AddAsync(product);
                
                // Reload the product to get all navigation properties
                var createdProduct = await _productRepository.GetByIdAsync(product.ProductId);
                return createdProduct!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product: {ProductName}", product?.ProductName);
                throw;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));

                var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {product.ProductId} not found");
                }

                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product: {ProductId}", product?.ProductId);
                throw;
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found");
                }

                await _productRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> UpdateProductImagesAsync(int productId, List<string> imageUrls)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {Id} not found for image update", productId);
                    return false;
                }

                // Clear existing images
                if (product.Images != null)
                {
                    product.Images.Clear();
                }
                else
                {
                    product.Images = new List<ProductImage>();
                }

                // Add new images
                foreach (var url in imageUrls)
                {
                    product.Images.Add(new ProductImage
                    {
                        ProductId = productId,
                        ImageUrl = url,
                        IsMainImage = product.Images.Count == 0 // First image is main image
                    });
                }

                await _productRepository.UpdateAsync(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating images for product {ProductId}", productId);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetAllProductsAsync();
                }

                return await _productRepository.SearchProductsAsync(searchTerm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products with term {SearchTerm}", searchTerm);
                throw;
            }
        }
    }
}
