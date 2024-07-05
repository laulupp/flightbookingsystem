using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;

namespace Backend.Profiles;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<FlightDTO, Flight>().ReverseMap();
        CreateMap<AircraftDTO, Aircraft>().ReverseMap();
        CreateMap<AirportDTO, Airport>().ReverseMap();
    }
}