using Catalog.Application.Queries;
using Catalog.Domain;
using MediatR;
using MongoDB.Driver;

namespace Catalog.Application.Handlers;

public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, List<Event>>
{
    private readonly ICatalogContext _context;

    public GetAllEventsQueryHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        // از طریق اینترفیس به دیتابیس دسترسی پیدا می‌کنیم و تمام اسناد را می‌خوانیم
        return await _context.Events.Find(_ => true).ToListAsync(cancellationToken);
    }
}
