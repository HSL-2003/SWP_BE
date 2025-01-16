public class Promotion
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DiscountType { get; set; } // Percentage/FixedAmount
    public decimal DiscountValue { get; set; }
    public decimal? MinimumPurchaseAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PromoCode { get; set; }
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; }
    public bool IsActive { get; set; }
}

public class LoyaltyProgram
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal PointsPerDollar { get; set; }
    public decimal DollarsPerPoint { get; set; }
    public List<LoyaltyTier> Tiers { get; set; }
} 