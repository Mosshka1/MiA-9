using Models;

namespace Dtos;

public class GameSessionDto
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public SessionStatus Status { get; set; }
    public string? Notes { get; set; }
}

public class CreateGameSessionDto
{
    public int BookingId { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Scheduled;
    public string? Notes { get; set; }
}
