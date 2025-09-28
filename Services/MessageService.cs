using RabbitInt.Brokers.Contracts;
using RabbitInt.Clients.Contracts;

namespace RabbitInt.Services
{
    internal class MessageService : IMessageService
    {
        private readonly IRabbitMQBroker _rabbitBroker;

        public MessageService(IRabbitMQBroker rabbitBroker) =>
            _rabbitBroker = rabbitBroker;

        /// <inheritdoc />
        public async Task SendAsync<T>(string exchange, string queue, T message) where T : class
        {
            ArgumentNullException.ThrowIfNull(message);

            if (string.IsNullOrWhiteSpace(exchange))
                throw new ArgumentNullException(nameof(exchange));

            if (string.IsNullOrWhiteSpace(queue))
                throw new ArgumentNullException(nameof(queue));

            await _rabbitBroker.PublishToQueueAsync(exchange, queue, message)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task SendAsync<T>(string queue, T message) where T : class
        {
            ArgumentNullException.ThrowIfNull(message);

            if (string.IsNullOrWhiteSpace(queue))
                throw new ArgumentNullException(nameof(queue));

            await _rabbitBroker.PublishToQueueAsync(string.Empty, queue, message)
                .ConfigureAwait(false);
        }
    }
}
