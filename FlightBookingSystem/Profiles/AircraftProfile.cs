using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;

namespace Backend.Profiles;

public class AircraftProfile : Profile
{
    public AircraftProfile()
    {
        CreateMap<AircraftDTO, Aircraft>().ReverseMap();
    }
}
