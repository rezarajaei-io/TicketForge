using RabbitMQ.Client;
using ReservationService.Application.Interfaces.RabbitMQ;
using System.Text;
using System.Text.Json;

namespace ReservationService.Infrastructure.Messaging;

public class RabbitMqBus : IMessageBus
{
    // Method signature is now async
    public async Task PublishAsync<T>(T message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        // Use 'await using' for IAsyncDisposable resources
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync(); // CreateChannel is also async now

        var eventName = typeof(T).Name;
        // The queue declaration can be wrapped in an async method if needed,
        // but for now, this sync call inside the async method is fine.
        channel.QueueDeclareAsync(queue: eventName, durable: true, exclusive: false, autoDelete: false);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        // BasicPublish is still synchronous as it just places the message in an outbound buffer.
        channel.BasicPublishAsync(exchange: "", routingKey: eventName, body: body);

        Console.WriteLine($"MESSAGE BUS: Published event '{eventName}' to RabbitMQ.");

        // No return is needed as the method returns Task, not Task<T>
    }
}