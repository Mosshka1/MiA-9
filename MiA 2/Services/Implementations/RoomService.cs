using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repo;

    public RoomService(IRoomRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Room>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Room?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task<Room> CreateAsync(Room room) => _repo.CreateAsync(room);

    public Task<Room?> UpdateAsync(int id, Room room) => _repo.UpdateAsync(id, room);

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}
