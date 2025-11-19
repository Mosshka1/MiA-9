using Models;

namespace Repositories.Interfaces;

public interface ISessionRepository
{
    Task<List<GameSession>> GetAllAsync();
    Task<GameSession?> GetByIdAsync(int id);
    Task<GameSession> CreateAsync(GameSession session);
    Task<GameSession?> UpdateAsync(int id, GameSession session);
    Task<bool> DeleteAsync(int id);
}
