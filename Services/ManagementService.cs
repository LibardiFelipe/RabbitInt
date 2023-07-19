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
    }
}
