using RabbitInt.Clients.Models;
using RabbitMQ.Client.Events;

namespace RabbitInt.Clients.Contracts
{
    public interface IManagementService
    {
        void DeclareQueues(List<RabbitIntQueue> queues);
        void BindConsumer<T>(string queue, bool autoAck, Action<object, BasicDeliverEventArgs, T> @delegate);
    }
}
