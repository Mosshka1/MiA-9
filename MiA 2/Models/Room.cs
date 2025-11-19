using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models;

public class Room
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string MongoId { get; set; } = null!;      

    public int Id { get; set; }                      
    public string Name { get; set; } = "";
    public int Capacity { get; set; }
    public bool Active { get; set; } = true;
}
