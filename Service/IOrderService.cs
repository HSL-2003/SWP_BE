using Data.Models;
using Data.OrderDTO;
using static Repo.OrderRepository;


namespace Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<OrderInfo> GetOrderInfoAsync(int orderId);
        Task<OrderDetailInfo> GetOrderDetailInfoAsync(int orderId);
    }
}
