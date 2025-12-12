using ReservationService.Application.Interfaces;
using ReservationService.Domain.Entities;

namespace ReservationService.Infrastructure.Services;

public class ReservationServiceReal : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationServiceReal(IReservationRepository repository)
    {
        _repository = repository;
    }

    // Reserve Logic
    public async Task<Reservation> CreateReservationAsync(string userId, string eventId, int quantity)
    {
        var reservation = Reservation.Create(userId, eventId, quantity);
        await _repository.AddAsync(reservation);
        return reservation;
    }
}
