using Data;
using Models;
using MongoDB.Driver;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class SessionRepository : ISessionRepository
{
    private readonly IMongoCollection<GameSession> _collection;

    public SessionRepository(MongoDbContext context)
    {
        _collection = context.Sessions;
    }

    public async Task<List<GameSession>> GetAllAsync()
        => await _collection.Find(_ => true).ToListAsync();

    public async Task<GameSession?> GetByIdAsync(int id)
        => await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();

    public async Task<GameSession> CreateAsync(GameSession session)
    {
        var last = await _collection.Find(FilterDefinition<GameSession>.Empty)
            .SortByDescending(s => s.Id)
            .Limit(1)
            .FirstOrDefaultAsync();

        session.Id = (last?.Id ?? 0) + 1;

        await _collection.InsertOneAsync(session);
        return session;
    }

    public async Task<GameSession?> UpdateAsync(int id, GameSession session)
    {
        var existing = await GetByIdAsync(id);
        if (existing is null) return null;

        session.MongoId = existing.MongoId;
        session.Id = existing.Id;

        var result = await _collection.ReplaceOneAsync(s => s.Id == id, session);
        return result.MatchedCount == 0 ? null : session;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _collection.DeleteOneAsync(s => s.Id == id);
        return result.DeletedCount > 0;
    }
}
