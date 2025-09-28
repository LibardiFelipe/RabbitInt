using RabbitInt.Brokers.Contracts;
using RabbitInt.Clients.Models;
using RabbitInt.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RabbitInt.Brokers
{
    internal class RabbitMQBroker : IRabbitMQBroker
    {
        private readonly IChannel _channel;

        public RabbitMQBroker(ConnectionFactory connFactory)
        {
            var connection = connFactory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();
        }

        public async Task PublishToQueueAsync<T>(string exchange, string routingKey, T body)
        {
            var properties = new BasicProperties();
            var bodyBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body, typeof(T), RabbitIntJsonContext.Default));
            await _channel.BasicPublishAsync(exchange, routingKey, mandatory: false, properties, bodyBytes)
                .ConfigureAwait(false);
        }

        public async Task DeclareQueuesAsync(List<RabbitIntQueue> queues)
        {
            foreach (var queue in queues)
            {
                await _channel
                    .QueueDeclareAsync(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments)
                    .ConfigureAwait(false);
            }
        }

        public async Task BindConsumerAsync<T>(string queue, bool autoAck, Action<object, T> @delegate)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var messageBody = ea.Body.ToArray();
                var deserializedMessage = (T)JsonSerializer.Deserialize(Encoding.UTF8.GetString(messageBody), typeof(T), RabbitIntJsonContext.Default);
                @delegate(sender, deserializedMessage);

                if (!autoAck)
                {
                    await _channel.BasicAckAsync(ea.DeliveryTag, false)
                        .ConfigureAwait(false);
                }
            };

            await _channel.BasicConsumeAsync(queue, autoAck, consumer)
                .ConfigureAwait(false);
        }

        public async Task BindConsumerAsync<T>(string queue, bool autoAck, Func<object, T, Task> asyncDelegate)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var messageBody = ea.Body.ToArray();
                var deserializedMessage = (T)JsonSerializer.Deserialize(Encoding.UTF8.GetString(messageBody), typeof(T), RabbitIntJsonContext.Default);
                await asyncDelegate(sender, deserializedMessage);

                if (!autoAck)
                {
                    await _channel.BasicAckAsync(ea.DeliveryTag, false)
                        .ConfigureAwait(false);
                }
            };

            await _channel.BasicConsumeAsync(queue, autoAck, consumer)
                .ConfigureAwait(false);
        }
    }
}
