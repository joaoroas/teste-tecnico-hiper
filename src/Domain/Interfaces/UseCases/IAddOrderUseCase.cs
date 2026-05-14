using Domain.Models.Entities;
using Domain.Models.Requests;
using Domain.Models.Response;

namespace Domain.Interfaces.UseCases
{
    public interface IAddOrderUseCase
    {
        Task<Result<Order>> AddOrderAsync(AddOrderRequest request);
    }
}
