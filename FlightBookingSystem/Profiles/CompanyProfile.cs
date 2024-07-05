using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;

namespace Backend.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyDTO, Company>().ReverseMap();
        CreateMap<CompanyRegistrationRequestDTO, CompanyRegistrationRequest>().ReverseMap();
    }
}