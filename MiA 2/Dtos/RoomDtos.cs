namespace Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Capacity { get; set; }
    public bool Active { get; set; }
}

public class CreateRoomDto
{
    public string Name { get; set; } = "";
    public int Capacity { get; set; }
    public bool Active { get; set; } = true;
}
