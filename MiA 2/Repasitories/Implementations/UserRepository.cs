using Data;
using Models;
using MongoDB.Driver;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(MongoDbContext context)
    {
        _collection = context.Users;
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();

    public async Task<User?> GetByIdAsync(int id)
        => await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task<User> CreateAsync(User user)
    {
        var last = await _collection.Find(FilterDefinition<User>.Empty)
            .SortByDescending(u => u.Id)
            .Limit(1)
            .FirstOrDefaultAsync();

        user.Id = (last?.Id ?? 0) + 1;

        await _collection.InsertOneAsync(user);
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var result = await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);
        return result.MatchedCount == 0 ? null : user;
    }
}
