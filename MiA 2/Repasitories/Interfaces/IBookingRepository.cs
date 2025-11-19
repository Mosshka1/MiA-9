using Models;

namespace Repositories.Interfaces;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking?> UpdateAsync(int id, Booking booking);
    Task<bool> DeleteAsync(int id);
}
