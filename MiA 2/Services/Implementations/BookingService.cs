using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repo;
    private readonly IRoomRepository _roomRepo;

    public BookingService(IBookingRepository repo, IRoomRepository roomRepo)
    {
        _repo = repo;
        _roomRepo = roomRepo;
    }

    public Task<List<Booking>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Booking?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task<Booking> CreateAsync(Booking booking)
    {
        var room = await _roomRepo.GetByIdAsync(booking.RoomId);
        if (room is null || !room.Active)
            throw new InvalidOperationException("Room is invalid or inactive.");

        if (booking.Players > room.Capacity)
            throw new InvalidOperationException("Players exceed room capacity.");

        var allBookings = await _repo.GetAllAsync();
        bool overlaps = allBookings.Any(b =>
            b.RoomId == booking.RoomId &&
            Math.Abs((b.StartUtc - booking.StartUtc).TotalMinutes) < 90
        );

        if (overlaps)
            throw new InvalidOperationException("Time slot overlaps with existing booking.");

        return await _repo.CreateAsync(booking);
    }

    public Task<Booking?> UpdateAsync(int id, Booking booking)
        => _repo.UpdateAsync(id, booking);

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(id);

    public async Task<Booking?> ApproveAsync(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return null;

        existing.Approved = true;
        return await _repo.UpdateAsync(id, existing);
    }
}
