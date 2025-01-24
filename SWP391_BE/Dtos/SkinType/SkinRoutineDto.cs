using System.ComponentModel.DataAnnotations;

namespace SWP391_BE.Dtos.SkinType
{
    public class SkinRoutineDto
    {
        public int SkinTypeId { get; set; }
        
        [Required(ErrorMessage = "Tên loại da không được để trống")]
        public string SkinTypeName { get; set; }
        
        [Required(ErrorMessage = "Phải có ít nhất một bước trong routine")]
        [MinLength(1, ErrorMessage = "Phải có ít nhất một bước trong routine")]
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