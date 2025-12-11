using Catalog.Application;
using Catalog.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure;

public class CatalogContext : ICatalogContext
{
    private readonly IMongoCollection<Event> _eventsCollection;

    public CatalogContext(IOptions<DatabaseSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _eventsCollection = mongoDatabase.GetCollection<Event>(dbSettings.Value.CollectionName);
    }

    public IMongoCollection<Event> Events => _eventsCollection;
}
