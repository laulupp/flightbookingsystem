using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;

namespace Backend.Profiles;

public class AirportProfile : Profile
{
    public AirportProfile()
    {
        CreateMap<AirportDTO, Airport>().ReverseMap();
    }
}
