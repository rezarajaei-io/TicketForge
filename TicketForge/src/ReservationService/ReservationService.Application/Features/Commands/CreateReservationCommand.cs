using MediatR;

namespace ReservationService.Application.Features.Commands;


public record CreateReservationCommand(
    string UserId,
    string EventId,
    int Quantity) : IRequest<Guid>;
