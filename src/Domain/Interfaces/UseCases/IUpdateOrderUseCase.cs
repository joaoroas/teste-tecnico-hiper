using Domain.Models.Requests;
using Domain.Models.Response;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateOrderUseCase
    {
        Task<Result<string>> UpdateOrderAsync(UpdateOrderRequest updateRequest);
    }
}
