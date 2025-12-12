using ReservationService.Application.Features.Commands;
using Grpc.Core;
using MediatR;

namespace ReservationService.API.Services;



public class ReservationerApiService : Reservationer.ReservationerBase
{
    private readonly IMediator _mediator;

    public ReservationerApiService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CreateReservationResponse> CreateReservation(
        CreateReservationRequest request, ServerCallContext context)
    {
        var command = new CreateReservationCommand(request.UserId, request.EventId, request.Quantity);

        var reservationId = await _mediator.Send(command);

        return new CreateReservationResponse { ReservationId = reservationId.ToString() };
    }
}

