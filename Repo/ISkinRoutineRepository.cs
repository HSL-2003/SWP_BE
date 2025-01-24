using Data.Models;

namespace Repo
{
    public interface ISkinRoutineRepository
    {
        Task<SkinRoutine> GetBySkinTypeIdAsync(int skinTypeId);
        Task<IEnumerable<SkinRoutine>> GetAllAsync();
        Task<SkinRoutine> AddAsync(SkinRoutine skinRoutine);
        Task UpdateAsync(SkinRoutine skinRoutine);
        Task DeleteAsync(int skinRoutineId);
    }
}
