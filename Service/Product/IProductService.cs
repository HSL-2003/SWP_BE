using SWP391_BE.Dtos.Product;

namespace Service.Product
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductCreateDto productDto);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<PagedResult<ProductDto>> GetProductsAsync(ProductFilterDto filter);
        Task<List<ProductDto>> GetRecommendedProductsAsync(int skinTypeId);
        Task<List<ProductDto>> CompareProductsAsync(List<int> productIds);
        Task UpdateProductAsync(int id, ProductCreateDto productDto);
        Task DeleteProductAsync(int id);
    }
} 