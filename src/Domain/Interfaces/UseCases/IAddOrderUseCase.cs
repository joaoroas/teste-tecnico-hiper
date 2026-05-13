using Domain.Entities;

namespace Domain.Interfaces.UseCases
{
    public interface IAddOrderUseCase
    {
        Task AddOrderAsync(Order order);
    }
}
