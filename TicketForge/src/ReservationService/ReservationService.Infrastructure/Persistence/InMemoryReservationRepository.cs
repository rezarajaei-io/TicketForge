using ReservationService.Application.Interfaces;
using ReservationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Infrastructure.Persistence;

public class InMemoryReservationRepository : IReservationRepository
{
    private static readonly List<Reservation> _reservations = new();

    public Task AddAsync(Reservation reservation)
    {
        _reservations.Add(reservation);
        Console.WriteLine($"REPOSITORY: Reservation {reservation.Id} added to in-memory store.");
        return Task.CompletedTask;
    }
}
