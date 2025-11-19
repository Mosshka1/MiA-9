using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models;

public class GameSession
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string MongoId { get; set; } = null!;

    public int Id { get; set; }

    public int BookingId { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Scheduled;
    public string? Notes { get; set; }
}
