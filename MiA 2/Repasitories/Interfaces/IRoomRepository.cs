using Models;

namespace Repositories.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task<Room> CreateAsync(Room room);
    Task<Room?> UpdateAsync(int id, Room room);
    Task<bool> DeleteAsync(int id);
}
