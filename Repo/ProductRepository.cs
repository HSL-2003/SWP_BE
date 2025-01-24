using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SWP391_BE.Dtos.Product;

namespace Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SuitableSkinTypes)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(ProductFilterDto filter)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.SuitableSkinTypes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(p => p.ProductName.Contains(filter.SearchTerm) || 
                                       p.Description.Contains(filter.SearchTerm));
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            if (filter.CategoryIds?.Any() == true)
            {
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId));
            }

            if (filter.SkinTypeId.HasValue)
            {
                query = query.Where(p => p.SuitableSkinTypes.Any(st => st.SkinTypeId == filter.SkinTypeId));
            }

            return await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetBySkinTypeIdAsync(int skinTypeId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SuitableSkinTypes)
                .Where(p => p.SuitableSkinTypes.Any(st => st.SkinTypeId == skinTypeId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(List<int> productIds)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SuitableSkinTypes)
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();
        }
    }
}
