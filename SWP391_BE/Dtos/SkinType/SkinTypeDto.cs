namespace SWP391_BE.Dtos.SkinType
{
    public class SkinTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Characteristics { get; set; }
    }
} 