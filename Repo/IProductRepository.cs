using Data.Models;

namespace Repo
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync(ProductFilterDto filter);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> GetBySkinTypeIdAsync(int skinTypeId);
        Task<IEnumerable<Product>> GetByIdsAsync(List<int> productIds);
    }
}
