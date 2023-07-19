namespace RabbitInt.Brokers.Contracts
{
    internal interface IRabbitMQBroker
    {
        Task PublishToQueueAsync<T>(string exchange, string routingKey, T body);
    }
}
