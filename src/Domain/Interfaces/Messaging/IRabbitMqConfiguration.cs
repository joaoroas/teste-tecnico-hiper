using RabbitMQ.Client;

namespace Domain.Interfaces.Messaging
{
    public interface IRabbitMqConfiguration
    {
        Task<IChannel> CreateChannelAsync();
    }
}