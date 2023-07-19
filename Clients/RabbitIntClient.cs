using Microsoft.Extensions.DependencyInjection;
using RabbitInt.Brokers;
using RabbitInt.Brokers.Contracts;
using RabbitInt.Clients.Contracts;
using RabbitInt.Contracts;
using RabbitInt.Services;
using RabbitMQ.Client;

namespace RabbitInt.Clients
{
    public sealed class RabbitIntClient : IRabbitIntClient
    {
        public RabbitIntClient(ConnectionFactory connFactory)
        {
            var serviceProvider = RegisterServices(connFactory);
            InitializeClients(serviceProvider);
        }

        public IMessageService Message { get; private set; }
        public IManagementService Management { get; private set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            Message = serviceProvider.GetService<IMessageService>();
            Management = serviceProvider.GetService<IManagementService>();
        }

        private static IServiceProvider RegisterServices(ConnectionFactory connFactory)
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton(connFactory)
                .AddTransient<IRabbitMQBroker, RabbitMQBroker>()
                .AddTransient<IManagementService, ManagementService>()
                .AddTransient<IMessageService, MessageService>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
