using RabbitMQ.Client;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Text;
using System.Text.Json;

namespace RentChallenge.Application.Messaging
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqPublisher(IRabbitMqConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }

        public async Task PublishAsync(string topic, object message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            await _channel.QueueDeclareAsync(topic, durable: true, exclusive: false, autoDelete: false);

            var basicProperties = new BasicProperties{ ContentType = "application/json" };

            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: topic,
                mandatory: false,
                basicProperties: basicProperties,
                body: body
            );
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}