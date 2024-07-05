using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Services.Interfaces;
using static Backend.Exceptions.CustomExceptions;

namespace Backend.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public FlightService(IFlightRepository flightRepository, IMapper mapper, ILogger<FlightService> logger)
    {
        _flightRepository = flightRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<FlightDTO>> GetAllFlightsAsync()
    {
        _logger.LogInformation("Retrieving all flights");
        var flights = await _flightRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FlightDTO>>(flights);
    }

    public async Task<FlightDTO?> GetFlightByIdAsync(int flightId)
    {
        _logger.LogInformation("Retrieving flight with ID: {FlightId}", flightId);
        var flight = await _flightRepository.GetByIdAsync(flightId);
        if (flight == null)
        {
            throw new FlightNotFoundException();
        }
        return _mapper.Map<FlightDTO>(flight);
    }

    public async Task<FlightDTO> AddFlightAsync(FlightDTO flightDto)
    {
        _logger.LogInformation("Adding new flight");
        var flight = _mapper.Map<Flight>(flightDto);
        return _mapper.Map<FlightDTO>(await _flightRepository.AddAsync(flight));
    }

    public async Task UpdateFlightAsync(FlightDTO flightDto)
    {
        _logger.LogInformation("Updating flight with ID: {FlightId}", flightDto.Id);
        var flight = await _flightRepository.GetByIdAsync(flightDto.Id);
        if (flight == null)
        {
            throw new FlightNotFoundException();
        }
        _mapper.Map(flightDto, flight);
        await _flightRepository.UpdateAsync(flight);
    }

    public async Task DeleteFlightAsync(int flightId)
    {
        _logger.LogInformation("Deleting flight with ID: {FlightId}", flightId);
        var flight = await _flightRepository.GetByIdAsync(flightId);
        if (flight == null)
        {
            throw new FlightNotFoundException();
        }
        await _flightRepository.DeleteAsync(flightId);
    }

    public async Task<IEnumerable<FlightDTO>> SearchFlightsAsync(FlightSearchParams dto)
    {
        _logger.LogInformation("Searching flights with parameters");
        var flights = await _flightRepository.SearchFlightsAsync(dto);
        return _mapper.Map<IEnumerable<FlightDTO>>(flights);
    }
}
