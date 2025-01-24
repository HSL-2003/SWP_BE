using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class SkinRoutine
{
    public int Id { get; set; }

    public int SkinTypeId { get; set; }

    public virtual Skintype SkinType { get; set; } = null!;

    public List<RoutineStep> Steps { get; set; }
}

public class RoutineStep
{
    public int Id { get; set; }
    public int StepOrder { get; set; }
    public string StepName { get; set; }
    public string Description { get; set; }
    public ICollection<Product> RecommendedProducts { get; set; }
}
