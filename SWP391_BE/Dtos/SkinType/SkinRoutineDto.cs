namespace SWP391_BE.Dtos.SkinType
{
    public class SkinRoutineDto
    {
        public int SkinTypeId { get; set; }
        public string SkinTypeName { get; set; }
        public List<RoutineStepDto> Steps { get; set; }
    }

    public class RoutineStepDto
    {
        public int StepOrder { get; set; }
        public string StepName { get; set; }
        public string Description { get; set; }
        public List<ProductDto> RecommendedProducts { get; set; }
    }
} 