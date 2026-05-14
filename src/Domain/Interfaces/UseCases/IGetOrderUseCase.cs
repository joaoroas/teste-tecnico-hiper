using Domain.Models.Entities;
using Domain.Models.Response;

namespace Domain.Interfaces.UseCases
{
    public interface IGetOrderUseCase
    {
        Task<Result<List<Order>>> GetOrdersAsync();
        Task<Result<Order>> GetOrderByIdAsync(int orderId);
    }
}
