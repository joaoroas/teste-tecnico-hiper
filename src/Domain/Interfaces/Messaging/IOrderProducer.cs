using Domain.Models.Messaging;

namespace Domain.Interfaces.Messaging
{
    public interface IOrderProducer
    {
        Task SendMessageAsync(OrderMessage message);
    }
}
