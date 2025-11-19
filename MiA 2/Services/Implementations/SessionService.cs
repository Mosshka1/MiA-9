using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _repo;
    private readonly IBookingRepository _bookingRepo;

    public SessionService(ISessionRepository repo, IBookingRepository bookingRepo)
    {
        _repo = repo;
        _bookingRepo = bookingRepo;
    }

    public Task<List<GameSession>> GetAllAsync() => _repo.GetAllAsync();

    public Task<GameSession?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task<GameSession> CreateAsync(GameSession session)
    {
        var booking = await _bookingRepo.GetByIdAsync(session.BookingId);
        if (booking is null || !booking.Approved)
            throw new InvalidOperationException("Booking must exist and be approved.");

        session.Status = SessionStatus.Scheduled;
        return await _repo.CreateAsync(session);
    }

    public Task<GameSession?> UpdateAsync(int id, GameSession session)
        => _repo.UpdateAsync(id, session);

    public Task<bool> DeleteAsync(int id)
        => _repo.DeleteAsync(id);

    public async Task<GameSession?> SetStatusAsync(int id, SessionStatus status)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return null;

        existing.Status = status;
        return await _repo.UpdateAsync(id, existing);
    }
}
