using Catalog.Domain;
using MongoDB.Driver;

namespace Catalog.Application;

public interface ICatalogContext
{
    IMongoCollection<Event> Events { get; }
}
