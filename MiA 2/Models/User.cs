using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string MongoId { get; set; } = null!;

    public int Id { get; set; }

    public string Email { get; set; } = "";
    public string Password { get; set; } = ""; 
    public UserRoles Role { get; set; } = UserRoles.User;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
