using Data.Models;
using Data.OrderDTO;
using Microsoft.EntityFrameworkCore;
using Repo;


namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly SkinCareManagementDbContext _context;
        public OrderService(IOrderRepository orderRepository, SkinCareManagementDbContext context)
        {
            _orderRepository = orderRepository;
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        public async Task<OrderInfo> GetOrderInfoAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (order == null)
            {
                throw new Exception("Đơn hàng không tồn tại.");
            }

            var orderInfo = new OrderInfo
            {
                Status = order.Status,
                Shipper = order.Shipper,
                TrackingCode = order.TrackingCode
            };

            return orderInfo;
        }

        public async Task<OrderDetailInfo> GetOrderDetailInfoAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (order == null)
            {
                throw new Exception("Đơn hàng không tồn tại.");
            }

            var orderDetailInfo = new OrderDetailInfo
            {
                OrderId = order.OrderId,
                Shipper = order.Shipper,
                Status = order.Status,
                TrackingCode = order.TrackingCode,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount
            };

            return orderDetailInfo;
        }
    }
    }

