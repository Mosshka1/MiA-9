using Data;
using Models;
using MongoDB.Driver;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class RoomRepository : IRoomRepository
{
    private readonly IMongoCollection<Room> _collection;

    public RoomRepository(MongoDbContext context)
    {
        _collection = context.Rooms;
    }

    public async Task<List<Room>> GetAllAsync()
        => await _collection.Find(_ => true).ToListAsync();

    public async Task<Room?> GetByIdAsync(int id)
        => await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();

    public async Task<Room> CreateAsync(Room room)
    {
        var last = await _collection.Find(FilterDefinition<Room>.Empty)
            .SortByDescending(r => r.Id)
            .Limit(1)
            .FirstOrDefaultAsync();

        room.Id = (last?.Id ?? 0) + 1;

        await _collection.InsertOneAsync(room);
        return room;
    }

    public async Task<Room?> UpdateAsync(int id, Room room)
    {
        var existing = await GetByIdAsync(id);
        if (existing is null) return null;

        room.MongoId = existing.MongoId;
        room.Id = existing.Id;

        var result = await _collection.ReplaceOneAsync(r => r.Id == id, room);
        return result.MatchedCount == 0 ? null : room;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _collection.DeleteOneAsync(r => r.Id == id);
        return result.DeletedCount > 0;
    }
}
