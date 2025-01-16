public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string OrderStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public PaymentInfo PaymentInfo { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? CompletedDate { get; set; }
} 