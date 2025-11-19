using Models;

namespace Dtos;

public class ScenarioDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int DurationMinutes { get; set; }
    public Difficulty Difficulty { get; set; }
    public int RoomId { get; set; }
}

public class CreateScenarioDto
{
    public string Title { get; set; } = "";
    public int DurationMinutes { get; set; }
    public Difficulty Difficulty { get; set; }
    public int RoomId { get; set; }
}
