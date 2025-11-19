using Models;

namespace Data;

public class InMemoryDb
{
    public List<Room> Rooms { get; } = new();
    public List<Scenario> Scenarios { get; } = new();
    public List<Booking> Bookings { get; } = new();
    public List<GameSession> Sessions { get; } = new();

    public int NextRoomId { get; private set; } = 0;
    public int NextScenarioId { get; private set; } = 0;
    public int NextBookingId { get; private set; } = 0;
    public int NextSessionId { get; private set; } = 0;

    public InMemoryDb()
    {
        var room1 = new Room { Id = ++NextRoomId, Name = "Pharaoh's Tomb", Capacity = 6, Active = true };
        var room2 = new Room { Id = ++NextRoomId, Name = "Cyber Heist", Capacity = 5, Active = true };
        Rooms.AddRange(new[] { room1, room2 });
        var scen1 = new Scenario
        {
            Id = ++NextScenarioId,
            Title = "Beginner Tomb",
            DurationMinutes = 60,
            Difficulty = Difficulty.Medium,
            RoomId = room1.Id
        };
        var scen2 = new Scenario
        {
            Id = ++NextScenarioId,
            Title = "Hardcore Heist",
            DurationMinutes = 75,
            Difficulty = Difficulty.Hard,
            RoomId = room2.Id
        };
        Scenarios.AddRange(new[] { scen1, scen2 });
        var b1 = new Booking
        {
            Id = ++NextBookingId,
            RoomId = room1.Id,
            StartUtc = DateTime.UtcNow.AddHours(2),
            Players = 4,
            CustomerName = "Taras Hrytsenko",
            CustomerPhone = "+380671112233",
            Approved = true
        };

        var b2 = new Booking
        {
            Id = ++NextBookingId,
            RoomId = room2.Id,
            StartUtc = DateTime.UtcNow.AddHours(4),
            Players = 3,
            CustomerName = "Oksana S.",
            CustomerPhone = "+380931234567",
            Approved = false
        };

        Bookings.AddRange(new[] { b1, b2 });
        var s1 = new GameSession
        {
            Id = ++NextSessionId,
            BookingId = b1.Id,
            Status = SessionStatus.Scheduled,
            Notes = "Seeded session"
        };
        Sessions.Add(s1);
    }

    public int NewRoomId() => ++NextRoomId;
    public int NewScenarioId() => ++NextScenarioId;
    public int NewBookingId() => ++NextBookingId;
    public int NewSessionId() => ++NextSessionId;
}
