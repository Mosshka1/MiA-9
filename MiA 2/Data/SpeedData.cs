using Models;

namespace Data;

public static class SeedData
{
    public static void EnsureSeed(InMemoryDb db)
    {
        if (db.Rooms.Any()) return; 

        var r1 = new Room { Id = db.NewRoomId(), Name = "Pharaoh's Tomb", Capacity = 6, Active = true };
        var r2 = new Room { Id = db.NewRoomId(), Name = "Cyber Heist", Capacity = 5, Active = true };
        db.Rooms.AddRange(new[] { r1, r2 });

        var s1 = new Scenario { Id = db.NewScenarioId(), Title = "Beginner Tomb", DurationMinutes = 60, Difficulty = Difficulty.Medium, RoomId = r1.Id };
        var s2 = new Scenario { Id = db.NewScenarioId(), Title = "Hardcore Heist", DurationMinutes = 75, Difficulty = Difficulty.Hard, RoomId = r2.Id };
        db.Scenarios.AddRange(new[] { s1, s2 });

        var b1 = new Booking
        {
            Id = db.NewBookingId(),
            RoomId = r1.Id,
            StartUtc = DateTime.UtcNow.AddHours(2),
            Players = 4,
            CustomerName = "Taras Hrytsenko",
            CustomerPhone = "+380671112233",
            Approved = true
        };
        var b2 = new Booking
        {
            Id = db.NewBookingId(),
            RoomId = r2.Id,
            StartUtc = DateTime.UtcNow.AddHours(4),
            Players = 3,
            CustomerName = "Oksana S.",
            CustomerPhone = "+380931234567",
            Approved = false
        };
        db.Bookings.AddRange(new[] { b1, b2 });

        var g1 = new GameSession { Id = db.NewSessionId(), BookingId = b1.Id, Status = SessionStatus.Scheduled, Notes = "Seeded session" };
        db.Sessions.Add(g1);
    }
}
