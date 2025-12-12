using ReservationService.Application.Features.Commands;

namespace ReservationService.Application.Features.Handlers;

using MediatR;
using ReservationService.Application.Events;
using ReservationService.Application.Interfaces;
using ReservationService.Application.Interfaces.RabbitMQ;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IReservationService _reservationService; // This will be the Proxy
    private readonly IMessageBus _messageBus;

    public CreateReservationCommandHandler(IReservationService reservationService, IMessageBus messageBus)
    {
        _reservationService = reservationService;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        // The handler doesn't know it's talking to a proxy. It just uses the interface.
        var reservation = await _reservationService.CreateReservationAsync(
            request.UserId,
            request.EventId,
            request.Quantity);

        // If successful, publish the integration event
        var reservationEvent = new ReservationRequestedEvent(
            reservation.Id,
            reservation.UserId,
            reservation.EventId,
            reservation.Quantity);

        await _messageBus.PublishAsync(reservationEvent);

        return reservation.Id;
    }
}

