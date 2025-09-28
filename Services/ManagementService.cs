using RabbitInt.Brokers.Contracts;
using RabbitInt.Clients.Contracts;
using RabbitInt.Clients.Models;

namespace RabbitInt.Services
{
    internal sealed class ManagementService
        : IManagementService
    {
        private readonly IRabbitMQBroker _rabbitBroker;

        public ManagementService(IRabbitMQBroker rabbitBroker) =>
             _rabbitBroker = rabbitBroker;

        public Task DeclareQueuesAsync(List<RabbitIntQueue> queues) =>
            _rabbitBroker.DeclareQueuesAsync(queues);

        public Task BindConsumerAsync<T>(string queue, bool autoAck, Action<object, T> @delegate) =>
           _rabbitBroker.BindConsumerAsync(queue, autoAck, @delegate);

        public Task BindConsumerAsync<T>(string queue, bool autoAck, Func<object, T, Task> asyncDelegate) =>
           _rabbitBroker.BindConsumerAsync(queue, autoAck, asyncDelegate);
    }
}
