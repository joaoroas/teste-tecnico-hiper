using Domain.Interfaces.Messaging;
using Domain.Models.Messaging;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace WebApi.Worker
{
    public class OrdersConsumer : BackgroundService
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;

        public OrdersConsumer(IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = await _rabbitMqConfiguration.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "orders_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += ConsumerReceived;

            await channel.BasicConsumeAsync(queue: "orders_queue", autoAck: true, consumerTag: "", consumer: consumer, noLocal: false, exclusive: false, arguments: null);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(60000, stoppingToken);
            }
        }

        private async Task ConsumerReceived(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                var message = JsonSerializer.Deserialize<OrderMessage>(Encoding.UTF8.GetString(args.Body.ToArray()));
            }
            catch (Exception ex
            {

                throw;
            }
        }
    }
}
