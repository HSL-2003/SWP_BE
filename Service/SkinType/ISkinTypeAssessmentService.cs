using SWP391_BE.Dtos.SkinType;

namespace Service.SkinType
{
    public interface ISkinTypeAssessmentService
    {
        Task<SkinTypeDto> DetermineSkinTypeAsync(SkinAssessmentDto assessment);
        Task<List<SkinQuestionDto>> GetAssessmentQuestionsAsync();
    }
} 