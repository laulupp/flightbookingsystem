using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;

namespace Backend.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterDTO, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(e => e.IsCompany ? Role.CompanyRepresentative : Role.Traveler));

        CreateMap<User, AuthResponseDTO>()
            .ForMember(dest => dest.Token, opt => opt.Ignore());
    }
}