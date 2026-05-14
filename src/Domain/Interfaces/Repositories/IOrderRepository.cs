using Domain.Models.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderByIdAsync(int orderId);

    }
}
