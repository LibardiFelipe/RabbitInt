using RabbitInt.Clients.Contracts;

namespace RabbitInt.Contracts
{
    internal interface IRabbitIntClient
    {
        IMessageService Message { get; }
        IManagementService Management { get; }
    }
}
