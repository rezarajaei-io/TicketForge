using Catalog.Api.Grpc; // using برای کدهای تولید شده از proto
using Catalog.Application.Queries; // using برای Query ما
using Grpc.Core;
using MediatR;

namespace Catalog.Api.Services;

public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CatalogGrpcService> _logger;

    public CatalogGrpcService(IMediator mediator, ILogger<CatalogGrpcService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<GetAllEventsResponse> GetAllEvents(GetAllEventsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("gRPC request received for GetAllEvents");

        // 1. ارسال Query از طریق MediatR
        var eventsFromDb = await _mediator.Send(new GetAllEventsQuery());

        // 2. ساخت پاسخ gRPC
        var response = new GetAllEventsResponse();

        // 3. تبدیل (Map) مدل Domain به مدل gRPC
        foreach (var ev in eventsFromDb)
        {
            response.Events.Add(new EventMessage
            {
                Id = ev.Id,
                Name = ev.Name,
                Description = ev.Description ?? "",
                EventDate = ev.EventDate.ToString("o"), // ISO 8601 format
                Venue = ev.Venue,
                AvailableTickets = ev.AvailableTickets
            });
        }

        return response;
    }
}
