using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Backend.Exceptions.CustomExceptions;

namespace Backend.Services;

public class AirportService : IAirportService
{
    private readonly ILogger _logger;
    private readonly IAirportRepository _airportRepository;
    private readonly IMapper _mapper;

    public AirportService(IAirportRepository airportRepository, IMapper mapper, ILogger<AirportService> logger)
    {
        _airportRepository = airportRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AirportDTO>> GetAllAirportsAsync()
    {
        _logger.LogInformation("Retrieving all airports");
        var airports = await _airportRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AirportDTO>>(airports);
    }

    public async Task<AirportDTO?> GetAirportByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving airport with ID: {AirportId}", id);
        var airport = await _airportRepository.GetByIdAsync(id);
        if (airport == null)
        {
            throw new LinkageNotFoundException();
        }
        return _mapper.Map<AirportDTO>(airport);
    }

    public async Task<AirportDTO> AddAirportAsync(AirportDTO airportDto)
    {
        _logger.LogInformation("Adding new airport: {AirportName}", airportDto.Name);
        var airport = _mapper.Map<Airport>(airportDto);
        return _mapper.Map<AirportDTO>(await _airportRepository.AddAsync(airport));
    }

    public async Task UpdateAirportAsync(AirportDTO airportDto)
    {
        _logger.LogInformation("Updating airport with ID: {AirportId}", airportDto.Id);
        var airport = await _airportRepository.GetByIdAsync(airportDto.Id);
        if (airport == null)
        {
            throw new LinkageNotFoundException();
        }
        _mapper.Map(airportDto, airport);
        await _airportRepository.UpdateAsync(airport);
    }

    public async Task DeleteAirportAsync(int id)
    {
        _logger.LogInformation("Deleting airport with ID: {AirportId}", id);
        var airport = await _airportRepository.GetByIdAsync(id);
        if (airport == null)
        {
            throw new LinkageNotFoundException();
        }
        await _airportRepository.DeleteAsync(id);
    }
}
