using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Models;
using Settings;

namespace Data;

public class MongoDbContext
{
    public IMongoCollection<Room> Rooms { get; }
    public IMongoCollection<Scenario> Scenarios { get; }
    public IMongoCollection<Booking> Bookings { get; }
    public IMongoCollection<GameSession> Sessions { get; }
    public IMongoCollection<User> Users { get; }

    public MongoDbContext(IOptions<MongoDbSettings> options)
    {
        var cfg = options.Value;
        var client = new MongoClient(cfg.ConnectionString);
        var db = client.GetDatabase(cfg.DatabaseName);

        Rooms = db.GetCollection<Room>("rooms");
        Scenarios = db.GetCollection<Scenario>("scenarios");
        Bookings = db.GetCollection<Booking>("bookings");
        Sessions = db.GetCollection<GameSession>("sessions");
        Users = db.GetCollection<User>("users");
    }
}
