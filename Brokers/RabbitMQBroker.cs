using System.Text;
using System.Text.Json;
using RabbitInt.Brokers.Contracts;
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
    }
}
