using MediatR;
using Catalog.Domain;

namespace Catalog.Application.Queries;

public class GetAllEventsQuery : IRequest<List<Event>>
{
}
