public class Customer
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string SkinType { get; set; }
    public int LoyaltyPoints { get; set; }
    public List<Order> Orders { get; set; }
    public DateTime CreatedAt { get; set; }
} 