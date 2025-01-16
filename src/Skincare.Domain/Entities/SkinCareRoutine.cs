public class SkinCareRoutine
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SkinType { get; set; }
    public List<RoutineStep> Steps { get; set; }
    public bool IsActive { get; set; }
}

public class RoutineStep
{
    public Guid Id { get; set; }
    public string StepName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public List<Product> RecommendedProducts { get; set; }
} 