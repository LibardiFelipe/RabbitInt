namespace RabbitInt.Clients.Models
{
    public sealed class RabbitQueue
    {
        public required string Name { get; set; }
        public required bool Durable { get; set; }
        public required bool Exclusive { get; set; }
        public required bool AutoDelete { get; set; }
        public Dictionary<string, object> Arguments { get; set; }
    }
}
