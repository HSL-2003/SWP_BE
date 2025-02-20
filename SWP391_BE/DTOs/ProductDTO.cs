namespace SWP391_BE.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public string? MainIngredients { get; set; }
        
        public int? BrandId { get; set; }
        public string? BrandName { get; set; }
        
        public int? VolumeId { get; set; }
        public string? VolumeValue { get; set; }
        
        public int? SkinTypeId { get; set; }
        public string? SkinTypeName { get; set; }
        
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

    public class CreateProductDTO
    {
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public string? MainIngredients { get; set; }
        public int? BrandId { get; set; }
        public int? VolumeId { get; set; }
        public int? SkinTypeId { get; set; }
        public int? CategoryId { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

    public class UpdateProductDTO
    {
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public string? MainIngredients { get; set; }
        public int? BrandId { get; set; }
        public int? VolumeId { get; set; }
        public int? SkinTypeId { get; set; }
        public int? CategoryId { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
} 