using RabbitInt.Clients.Models;
using RabbitMQ.Client.Events;

namespace RabbitInt.Brokers.Contracts
{
    internal interface IRabbitMQBroker
    {
        void DeclareQueues(List<RabbitIntQueue> queues);
        Task PublishToQueueAsync<T>(string exchange, string routingKey, T body);
        void BindConsumer<T>(string queue, bool autoAck, Action<object, BasicDeliverEventArgs, T> @delegate);
    }
}
