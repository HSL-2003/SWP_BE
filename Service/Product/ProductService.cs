using AutoMapper;
using SWP391_BE.Dtos.Product;
using Repo;

namespace Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto productDto)
        {
            var product = _mapper.Map<Data.Models.Product>(productDto);
            var result = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PagedResult<ProductDto>> GetProductsAsync(ProductFilterDto filter)
        {
            var products = await _productRepository.GetAllAsync(filter);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            
            // Implement paging logic here
            return new PagedResult<ProductDto>
            {
                Items = productDtos,
                TotalItems = productDtos.Count,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public async Task<List<ProductDto>> GetRecommendedProductsAsync(int skinTypeId)
        {
            var products = await _productRepository.GetBySkinTypeIdAsync(skinTypeId);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> CompareProductsAsync(List<int> productIds)
        {
            var products = await _productRepository.GetByIdsAsync(productIds);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task UpdateProductAsync(int id, ProductCreateDto productDto)
        {
            var product = _mapper.Map<Data.Models.Product>(productDto);
            product.ProductId = id;
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
} 