using Models;

namespace Services.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking?> UpdateAsync(int id, Booking booking);
    Task<bool> DeleteAsync(int id);
    Task<Booking?> ApproveAsync(int id);
}
