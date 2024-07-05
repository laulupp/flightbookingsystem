using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;

namespace Backend.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingDTO, Booking>().ReverseMap();
    }
}