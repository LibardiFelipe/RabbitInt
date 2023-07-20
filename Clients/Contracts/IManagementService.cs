using RabbitInt.Clients.Models;

namespace RabbitInt.Clients.Contracts
{
    public interface IManagementService
    {
        void DeclareQueues(List<RabbitQueue> queues);
        void BindConsumer<T>(string queue, bool autoAck, Action<object, T> @delegate);
    }
}
