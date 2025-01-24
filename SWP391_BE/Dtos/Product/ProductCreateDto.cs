using System.ComponentModel.DataAnnotations;

namespace SWP391_BE.Dtos.Product
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "URL hình ảnh không được để trống")]
        [Url(ErrorMessage = "URL hình ảnh không hợp lệ")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Danh mục không được để trống")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Phải chọn ít nhất một loại da phù hợp")]
        public List<int> SuitableSkinTypeIds { get; set; }
    }
} 