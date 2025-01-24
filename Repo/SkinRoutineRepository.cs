using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repo
{
    public class SkinRoutineRepository : ISkinRoutineRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SkinRoutineRepository> _logger;

        public SkinRoutineRepository(
            ApplicationDbContext context,
            ILogger<SkinRoutineRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SkinRoutine> GetBySkinTypeIdAsync(int skinTypeId)
        {
            try
            {
                return await _context.SkinRoutines
                    .Include(sr => sr.SkinType)
                    .Include(sr => sr.Steps)
                        .ThenInclude(s => s.RecommendedProducts)
                    .FirstOrDefaultAsync(sr => sr.SkinTypeId == skinTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting skin routine for skin type {SkinTypeId}", skinTypeId);
                throw;
            }
        }

        public async Task<IEnumerable<SkinRoutine>> GetAllAsync()
        {
            try
            {
                return await _context.SkinRoutines
                    .Include(sr => sr.SkinType)
                    .Include(sr => sr.Steps)
                        .ThenInclude(s => s.RecommendedProducts)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all skin routines");
                throw;
            }
        }

        public async Task<SkinRoutine> AddAsync(SkinRoutine skinRoutine)
        {
            try
            {
                _context.SkinRoutines.Add(skinRoutine);
                await _context.SaveChangesAsync();
                return skinRoutine;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding skin routine");
                throw;
            }
        }

        public async Task UpdateAsync(SkinRoutine skinRoutine)
        {
            try
            {
                _context.Entry(skinRoutine).State = EntityState.Modified;
                foreach (var step in skinRoutine.Steps)
                {
                    _context.Entry(step).State = step.Id == 0 ? 
                        EntityState.Added : EntityState.Modified;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating skin routine {Id}", skinRoutine.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var skinRoutine = await _context.SkinRoutines
                    .Include(sr => sr.Steps)
                    .FirstOrDefaultAsync(sr => sr.Id == id);

                if (skinRoutine != null)
                {
                    _context.SkinRoutines.Remove(skinRoutine);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting skin routine {Id}", id);
                throw;
            }
        }
    }
}
