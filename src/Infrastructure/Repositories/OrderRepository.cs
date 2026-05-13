using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IList<Order> orders = new List<Order>();
        public async Task AddOrderAsync(Order order)
        {
            orders.Add(order);
        }
    }
}
