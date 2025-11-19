using AutoMapper;
using Dtos;
using Models;

namespace Mapping;

public class MiAMappingProfile : Profile
{
    public MiAMappingProfile()
    {
        // Room
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<CreateRoomDto, Room>();

        // Scenario
        CreateMap<Scenario, ScenarioDto>().ReverseMap();
        CreateMap<CreateScenarioDto, Scenario>();

        // Booking
        CreateMap<Booking, BookingDto>().ReverseMap();
        CreateMap<CreateBookingDto, Booking>();

        // GameSession
        CreateMap<GameSession, GameSessionDto>().ReverseMap();
        CreateMap<CreateGameSessionDto, GameSession>();
    }
}
