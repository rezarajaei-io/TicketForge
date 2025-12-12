using ReservationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Application.Interfaces;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation);
}

public interface IReservationService
{
    Task<Reservation> CreateReservationAsync(string userId, string eventId, int quantity);
}
