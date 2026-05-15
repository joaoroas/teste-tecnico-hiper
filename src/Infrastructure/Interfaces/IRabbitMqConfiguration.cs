using RabbitMQ.Client;

namespace Infrastructure.Interfaces
{
    public interface IRabbitMqConfiguration
    {
        Task<IChannel> CreateChannelAsync();
    }
}