using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;

namespace Backend.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>().ForMember(u => u.Password, opts => opts.Ignore());
        CreateMap<UserDTO, User>().ForMember(u => u.Password, opts => opts.Ignore());
    }
}