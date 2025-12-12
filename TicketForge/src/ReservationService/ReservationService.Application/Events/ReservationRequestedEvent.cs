
namespace ReservationService.Application.Events;

public record ReservationRequestedEvent(Guid ReservationId, string UserId, string EventId, int Quantity);
