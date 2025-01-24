using Data.Models;

namespace Repo
{
    public interface ISkinTypeRepository
    {
        Task<SkinType> GetByIdAsync(int id);
        Task<IEnumerable<SkinType>> GetAllAsync();
        Task<SkinType> AddAsync(SkinType skinType);
        Task UpdateAsync(SkinType skinType);
        Task DeleteAsync(int id);
    }
}
