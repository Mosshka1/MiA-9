using Data;
using Models;
using MongoDB.Driver;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class ScenarioRepository : IScenarioRepository
{
    private readonly IMongoCollection<Scenario> _collection;

    public ScenarioRepository(MongoDbContext context)
    {
        _collection = context.Scenarios;
    }

    public async Task<List<Scenario>> GetAllAsync()
        => await _collection.Find(_ => true).ToListAsync();

    public async Task<Scenario?> GetByIdAsync(int id)
        => await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();

    public async Task<Scenario> CreateAsync(Scenario scenario)
    {
        var last = await _collection.Find(FilterDefinition<Scenario>.Empty)
            .SortByDescending(s => s.Id)
            .Limit(1)
            .FirstOrDefaultAsync();

        scenario.Id = (last?.Id ?? 0) + 1;

        await _collection.InsertOneAsync(scenario);
        return scenario;
    }

    public async Task<Scenario?> UpdateAsync(int id, Scenario scenario)
    {
        var existing = await GetByIdAsync(id);
        if (existing is null) return null;

        scenario.MongoId = existing.MongoId;
        scenario.Id = existing.Id;

        var result = await _collection.ReplaceOneAsync(s => s.Id == id, scenario);
        return result.MatchedCount == 0 ? null : scenario;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _collection.DeleteOneAsync(s => s.Id == id);
        return result.DeletedCount > 0;
    }
}
