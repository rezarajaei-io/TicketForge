using ReservationService.Application.Interfaces;
using ReservationService.Domain.Entities;
using ReservationService.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Infrastructure.Proxies
{
    public class ReservationServiceProxy : IReservationService
    {
        private readonly ReservationServiceReal _realService;
        // In a real scenario, this would be a gRPC client to CatalogService
        // private readonly ICatalogServiceGrpcClient _catalogClient; 

        public ReservationServiceProxy(ReservationServiceReal realService)
        {
            _realService = realService;
        }

        public async Task<Reservation> CreateReservationAsync(string userId, string eventId, int quantity)
        {
            // 1. Protection Logic (Proxy's responsibility)
            Console.WriteLine("PROXY: Checking conditions before creating reservation...");
            // For now, we assume the check passes. In a real app, you'd call CatalogService.
            // TODO : Temporarry -  فعلا موقت شبیه سازی میشه تا  proto دیزاین شود
            Console.WriteLine("PROXY: Conditions met. Delegating to the real service.");
            return await _realService.CreateReservationAsync(userId, eventId, quantity);
        }
    }

}
