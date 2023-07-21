using RabbitInt.Brokers.Contracts;
using RabbitInt.Clients.Contracts;
using RabbitInt.Clients.Models;
using RabbitMQ.Client.Events;

namespace RabbitInt.Services
{
    internal sealed class ManagementService
        : IManagementService
    {
        private readonly IRabbitMQBroker _rabbitBroker;

        public ManagementService(IRabbitMQBroker rabbitBroker) =>
             _rabbitBroker = rabbitBroker;

        public void DeclareQueues(List<RabbitIntQueue> queues) =>
            _rabbitBroker.DeclareQueues(queues);

        public void BindConsumer<T>(string queue, bool autoAck, Action<object, BasicDeliverEventArgs, T> @delegate) =>
           _rabbitBroker.BindConsumer(queue, autoAck, @delegate);
    }
}
