using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain;

public class Event
{
    // در MongoDB، معمولاً از استرینگ برای ID استفاده می‌شود تا ObjectId را نگه دارد.
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime EventDate { get; set; }

    public string Venue { get; set; } = string.Empty; // مکان برگزاری

    public int AvailableTickets { get; set; }
}
