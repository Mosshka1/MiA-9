using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models;

public class Scenario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string MongoId { get; set; } = null!;

    public int Id { get; set; }

    [Required, MinLength(3)]
    public string Title { get; set; } = "";

    [Range(1, 240)]
    public int DurationMinutes { get; set; } = 60;

    [Required]
    public Difficulty Difficulty { get; set; }

    [Required]
    public int RoomId { get; set; }   
}
