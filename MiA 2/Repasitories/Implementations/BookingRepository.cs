using Data;
using Models;
using MongoDB.Driver;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class BookingRepository : IBookingRepository
{
    private readonly IMongoCollection<Booking> _collection;

    public BookingRepository(MongoDbContext context)
    {
        _collection = context.Bookings;
    }

    public async Task<List<Booking>> GetAllAsync()
        => await _collection.Find(_ => true).ToListAsync();

    public async Task<Booking?> GetByIdAsync(int id)
        => await _collection.Find(b => b.Id == id).FirstOrDefaultAsync();

    public async Task<Booking> CreateAsync(Booking booking)
    {
        var last = await _collection.Find(FilterDefinition<Booking>.Empty)
            .SortByDescending(b => b.Id)
            .Limit(1)
            .FirstOrDefaultAsync();

        booking.Id = (last?.Id ?? 0) + 1;

        await _collection.InsertOneAsync(booking);
        return booking;
    }

    public async Task<Booking?> UpdateAsync(int id, Booking booking)
    {
        var existing = await GetByIdAsync(id);
        if (existing is null) return null;

        booking.MongoId = existing.MongoId;
        booking.Id = existing.Id;

        var result = await _collection.ReplaceOneAsync(b => b.Id == id, booking);
        return result.MatchedCount == 0 ? null : booking;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _collection.DeleteOneAsync(b => b.Id == id);
        return result.DeletedCount > 0;
    }
}
