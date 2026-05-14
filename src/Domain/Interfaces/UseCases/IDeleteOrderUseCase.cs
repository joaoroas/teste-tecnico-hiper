using Domain.Models.Response;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteOrderUseCase
    {
        Task<Result<string>> DeleteOrderAsync(int orderId);
    }
}
