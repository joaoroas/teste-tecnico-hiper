using Domain.Interfaces.Messaging;
using Domain.Models.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Messaging.Producers
{
    public class OrderProducer : IOrderProducer
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;

        public OrderProducer(IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }

        public async Task SendMessageAsync(OrderMessage message)
        {
            using var channel = await _rabbitMqConfiguration.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "orders_queue",
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false);

            var json = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: "orders_queue",
                                            mandatory: false,
                                            basicProperties: new BasicProperties(),
                                            body: body);
        }
    }
}
