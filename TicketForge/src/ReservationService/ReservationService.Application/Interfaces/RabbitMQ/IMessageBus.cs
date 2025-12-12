namespace ReservationService.Application.Interfaces.RabbitMQ;

public interface IMessageBus
{
    Task PublishAsync<T>(T message);
}
