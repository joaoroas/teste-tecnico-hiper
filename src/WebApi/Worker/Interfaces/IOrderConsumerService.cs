using Domain.Models.Messaging;

namespace WebApi.Worker.Interfaces
{
    public interface IOrderConsumerService
    {
        Task ProcessOrderAsync(OrderMessage orderMessage);
    }
}
