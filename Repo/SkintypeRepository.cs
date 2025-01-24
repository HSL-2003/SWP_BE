using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public class SkinTypeRepository : ISkinTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public SkinTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SkinType> GetByIdAsync(int id)
        {
            return await _context.SkinTypes
                .Include(st => st.SuitableProducts)
                .Include(st => st.SkinRoutines)
                .FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task<IEnumerable<SkinType>> GetAllAsync()
        {
            return await _context.SkinTypes
                .Include(st => st.SuitableProducts)
                .Include(st => st.SkinRoutines)
                .ToListAsync();
        }

        public async Task<SkinType> AddAsync(SkinType skinType)
        {
            _context.SkinTypes.Add(skinType);
            await _context.SaveChangesAsync();
            return skinType;
        }

        public async Task UpdateAsync(SkinType skinType)
        {
            _context.Entry(skinType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var skinType = await _context.SkinTypes.FindAsync(id);
            if (skinType != null)
            {
                _context.SkinTypes.Remove(skinType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
