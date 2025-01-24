using SWP391_BE.Dtos.SkinType;

namespace Service.SkinType
{
    public interface ISkinTypeService
    {
        Task<SkinTypeDto> GetSkinTypeByIdAsync(int id);
        Task<List<SkinTypeDto>> GetAllSkinTypesAsync();
        Task<SkinTypeDto> DetermineSkinTypeAsync(SkinAssessmentDto assessment);
        Task<SkinRoutineDto> GetSkinRoutineAsync(int skinTypeId);
        Task<List<ProductDto>> GetRecommendedProductsForSkinTypeAsync(int skinTypeId);
    }
} 