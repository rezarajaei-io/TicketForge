using ReservationService.Domain.Enums;
namespace ReservationService.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public string EventId { get; private set; }
    public int Quantity { get; private set; }
    public ReservationStatus Status { get; private set; }

    private Reservation(string userId, string eventId, int quantity)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        EventId = eventId;
        Quantity = quantity;
        Status = ReservationStatus.Pending;
    }

    public static Reservation Create(string userId, string eventId, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        return new Reservation(userId, eventId, quantity);
    }
}

