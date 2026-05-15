using Domain.Interfaces.Messaging;
using Infrastructure.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Messaging.Configuration
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private readonly RabbitMqSettings _rabbitMqSettings;

        public RabbitMqConfiguration(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                UserName = _rabbitMqSettings.User,
                Password = _rabbitMqSettings.Password
            };
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = await _factory.CreateConnectionAsync();
            }

            return await _connection.CreateChannelAsync();
        }
    }
}
