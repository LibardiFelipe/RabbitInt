using RabbitInt.Clients.Models;

namespace RabbitInt.Brokers.Contracts
{
    internal interface IRabbitMQBroker
    {
        Task DeclareQueuesAsync(List<RabbitIntQueue> queues);
        Task PublishToQueueAsync<T>(string exchange, string routingKey, T body);
        Task BindConsumerAsync<T>(string queue, bool autoAck, Action<object, T> @delegate);
        Task BindConsumerAsync<T>(string queue, bool autoAck, Func<object, T, Task> asyncDelegate);
    }
}
