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

        public void DeclareQueues(List<RabbitQueue> queues) =>
            _rabbitBroker.DeclareQueues(queues);

        public void BindConsumer<T>(string queue, bool autoAck, Action<object, T> @delegate) =>
           _rabbitBroker.BindConsumer(queue, autoAck, @delegate);
    }
}
