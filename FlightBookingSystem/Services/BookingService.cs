using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Services.Interfaces;
using static Backend.Exceptions.CustomExceptions;

namespace Backend.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper; 
    private readonly ILogger _logger;

    public BookingService(IBookingRepository bookingRepository, IMapper mapper, ILogger<BookingService> logger)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
    {
        _logger.LogInformation("Retrieving all bookings");
        var bookings = await _bookingRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
    }

    public async Task<BookingDTO?> GetBookingByIdAsync(int bookingId)
    {
        _logger.LogInformation("Retrieving booking with ID: {BookingId}", bookingId);
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null)
        {
            throw new BookingNotFoundException();
        }
        return _mapper.Map<BookingDTO>(booking);
    }

    public async Task<BookingDTO> AddBookingAsync(BookingDTO bookingDto)
    {
        _logger.LogInformation("Adding new booking");
        var booking = _mapper.Map<Booking>(bookingDto);
        return _mapper.Map<BookingDTO>(await _bookingRepository.AddAsync(booking));
    }

    public async Task UpdateBookingAsync(BookingDTO bookingDto)
    {
        _logger.LogInformation("Updating booking with ID: {BookingId}", bookingDto.Id);
        var booking = await _bookingRepository.GetByIdAsync(bookingDto.Id);
        if (booking == null)
        {
            throw new BookingNotFoundException();
        }
        _mapper.Map(bookingDto, booking);
        await _bookingRepository.UpdateAsync(booking);
    }

    public async Task DeleteBookingAsync(int bookingId)
    {
        _logger.LogInformation("Deleting booking with ID: {BookingId}", bookingId);
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null)
        {
            throw new BookingNotFoundException();
        }
        await _bookingRepository.DeleteAsync(bookingId);
    }

    public async Task<IEnumerable<BookingDTO>> GetBookingsByUsernameAsync(string username)
    {
        _logger.LogInformation("Retrieving bookings for username: {Username}", username);
        var bookings = await _bookingRepository.GetBookingsByUsernameAsync(username);
        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
    }
}
