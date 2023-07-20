using RabbitInt.Clients.Models;

namespace RabbitInt.Brokers.Contracts
{
    internal interface IRabbitMQBroker
    {
        void DeclareQueues(List<RabbitQueue> queues);
        Task PublishToQueueAsync<T>(string exchange, string routingKey, T body);
        void BindConsumer<T>(string queue, bool autoAck, Action<object, T> @delegate);
    }
}
