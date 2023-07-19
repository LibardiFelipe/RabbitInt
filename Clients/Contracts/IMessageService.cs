namespace RabbitInt.Clients.Contracts
{
    public interface IMessageService
    {
        /// <summary>
        /// Send a message to a specific exchange and queue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAsync<T>(string exchange, string queue, T message) where T : class;

        /// <summary>
        /// Send a message to a specific queue,
        /// without specifying any exchange.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAsync<T>(string queue, T message) where T : class;
    }
}
