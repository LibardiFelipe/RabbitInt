using System.Text;
using System.Text.Json;
using RabbitInt.Brokers.Contracts;
using RabbitInt.Clients.Models;
using RabbitMQ.Client;

namespace RabbitInt.Brokers
{
    internal class RabbitMQBroker : IRabbitMQBroker
    {
        private readonly IModel _model;

        public RabbitMQBroker(ConnectionFactory connFactory) =>
            _model = connFactory.CreateConnection().CreateModel();

        public async Task PublishToQueueAsync<T>(string exchange, string routingKey, T body) =>
            await Task.Run(() =>
                _model.BasicPublish(exchange, routingKey, basicProperties: null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body))));

        public void DeclareQueues(List<RabbitQueue> queues) =>
            queues.ForEach(queue =>
                _model.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments));
    }
}
