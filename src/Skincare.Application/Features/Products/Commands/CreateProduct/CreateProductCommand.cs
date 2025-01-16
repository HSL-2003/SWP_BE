public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }
    public string SkinType { get; set; }
    public List<string> Images { get; set; }
} 