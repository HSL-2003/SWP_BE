public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public PaymentInfoDto PaymentInfo { get; set; }
} 