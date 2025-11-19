using Models;

namespace Services.Interfaces;

public interface ISessionService
{
    Task<List<GameSession>> GetAllAsync();
    Task<GameSession?> GetByIdAsync(int id);
    Task<GameSession> CreateAsync(GameSession session);
    Task<GameSession?> UpdateAsync(int id, GameSession session);
    Task<bool> DeleteAsync(int id);
    Task<GameSession?> SetStatusAsync(int id, SessionStatus status);
}
