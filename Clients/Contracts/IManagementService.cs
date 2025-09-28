using RabbitInt.Clients.Models;

namespace RabbitInt.Clients.Contracts
{
    public interface IManagementService
    {
        Task DeclareQueuesAsync(List<RabbitIntQueue> queues);
        Task BindConsumerAsync<T>(string queue, bool autoAck, Action<object, T> @delegate);
        Task BindConsumerAsync<T>(string queue, bool autoAck, Func<object, T, Task> asyncDelegate);
    }
}
