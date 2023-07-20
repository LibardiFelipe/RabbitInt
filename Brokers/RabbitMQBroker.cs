using System.Text;
using System.Text.Json;
using RabbitInt.Brokers.Contracts;
using RabbitInt.Clients.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitInt.Brokers
{
    internal class RabbitMQBroker : IRabbitMQBroker
    {
        private readonly IModel _channel;

        public RabbitMQBroker(ConnectionFactory connFactory) =>
            _channel = connFactory.CreateConnection().CreateModel();

        public async Task PublishToQueueAsync<T>(string exchange, string routingKey, T body) =>
            await Task.Run(() =>
                _channel.BasicPublish(exchange, routingKey, basicProperties: null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body))));

        public void DeclareQueues(List<RabbitQueue> queues) =>
            queues.ForEach(queue =>
                _channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments));

        public void BindConsumer<T>(string queue, bool autoAck, Action<object, T> @delegate)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, ea) =>
                @delegate(sender, JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(ea.Body.ToArray())));

            _channel.BasicConsume(queue, autoAck, consumer);
        }
    }
}
