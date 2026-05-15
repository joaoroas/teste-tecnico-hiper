using Domain.Models.Messaging;
using Infrastructure.Interfaces;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using WebApi.Worker.Interfaces;

namespace WebApi.Worker.Consumers
{
    public class OrdersConsumer : BackgroundService
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IServiceProvider _serviceProvider;

        public OrdersConsumer(IRabbitMqConfiguration rabbitMqConfiguration, IServiceProvider serviceProvider)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var channel = await _rabbitMqConfiguration.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "orders_queue",
                                                durable: true,
                                                exclusive: false,
                                                autoDelete: false);


                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += ConsumerReceived;

                await channel.BasicConsumeAsync(queue: "orders_queue",
                                                autoAck: false,
                                                consumerTag: "",
                                                consumer: consumer,
                                                noLocal: false,
                                                exclusive: false,
                                                arguments: null);

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(60000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        private async Task ConsumerReceived(object sender, BasicDeliverEventArgs args)
        {
            var consumer = (AsyncEventingBasicConsumer)sender;
            var channel = consumer.Channel;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var orderConsumerService = scope.ServiceProvider.GetRequiredService<IOrderConsumerService>();

                var message = JsonSerializer.Deserialize<OrderMessage>(Encoding.UTF8.GetString(args.Body.ToArray()));

                

                await orderConsumerService.ProcessOrderAsync(message);

                await channel.BasicAckAsync(deliveryTag: args.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                await channel.BasicNackAsync(deliveryTag: args.DeliveryTag, multiple: false, requeue: true);
                throw;
            }
        }
    }
}
