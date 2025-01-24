using System.ComponentModel.DataAnnotations;

namespace SWP391_BE.Dtos.SkinType
{
    public class RoutineStepDto
    {
        [Required(ErrorMessage = "Thứ tự bước không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Thứ tự bước phải lớn hơn 0")]
        public int StepOrder { get; set; }

        [Required(ErrorMessage = "Tên bước không được để trống")]
        [StringLength(200, ErrorMessage = "Tên bước không được vượt quá 200 ký tự")]
        public string StepName { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Description { get; set; }

        public List<ProductDto> RecommendedProducts { get; set; }
    }
} 