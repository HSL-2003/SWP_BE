public class SkinTest
{
    public Guid Id { get; set; }
    public List<SkinTestQuestion> Questions { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SkinTestQuestion
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public List<SkinTestAnswer> PossibleAnswers { get; set; }
    public int OrderIndex { get; set; }
}

public class SkinTestAnswer
{
    public Guid Id { get; set; }
    public string Answer { get; set; }
    public int Score { get; set; }
    public string SkinTypeIndicator { get; set; }
} 