using Dapper;
using Domain.Interfaces.Repositories;
using Domain.Models.Entities;
using Infrastructure.DbContext;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbContext _dbContext;

        public OrderRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                var query = "INSERT INTO Orders (CustomerName, ProductName, Amount, OrderStatus, CreatedAt) VALUES (@CustomerName, @ProductName, @Amount, @OrderStatus, @CreatedAt)";

                var parameters = new DynamicParameters();
                parameters.Add("@CustomerName", order.CustomerName);
                parameters.Add("@ProductName", order.ProductName);
                parameters.Add("@Amount", order.Amount);
                parameters.Add("@OrderStatus", order.OrderStatus);
                parameters.Add("@CreatedAt", order.CreatedAt);

                using var connection = _dbContext.CreateConnection();

                await connection.ExecuteAsync(query, parameters);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            try
            {
                var query = "SELECT OrderId, CustomerName, ProductName, Amount, OrderStatus, CreatedAt FROM Orders"; ;

                using var connection = _dbContext.CreateConnection();

                var orders = await connection.QueryAsync<Order>(query);

                return orders.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var query = "SELECT * FROM Orders WHERE OrderId = @OrderId";

                using var connection = _dbContext.CreateConnection();

                var order = await connection.QueryFirstOrDefaultAsync<Order>(query, new { OrderId = orderId });

                return order;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}