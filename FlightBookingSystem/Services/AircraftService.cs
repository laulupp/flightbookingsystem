using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Backend.Exceptions.CustomExceptions;

namespace Backend.Services;

public class AircraftService : IAircraftService
{
    private readonly IAircraftRepository _aircraftRepository;
    private readonly IMapper _mapper;

    public AircraftService(IAircraftRepository aircraftRepository, IMapper mapper)
    {
        _aircraftRepository = aircraftRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AircraftDTO>> GetAllAircraftsAsync(int companyId)
    {
        var aircrafts = await _aircraftRepository.GetAircraftsByCompanyIdAsync(companyId);
        return _mapper.Map<IEnumerable<AircraftDTO>>(aircrafts);
    }

    public async Task<AircraftDTO?> GetAircraftByIdAsync(int id)
    {
        var aircraft = await _aircraftRepository.GetByIdAsync(id);

        return _mapper.Map<AircraftDTO>(aircraft);
    }

    public async Task<AircraftDTO> AddAircraftAsync(AircraftDTO aircraftDto, int companyId)
    {
        var aircraft = _mapper.Map<Aircraft>(aircraftDto);
        aircraft.CompanyId = companyId;
        return _mapper.Map<AircraftDTO>(await _aircraftRepository.AddAsync(aircraft));
    }

    public async Task UpdateAircraftAsync(AircraftDTO aircraftDto, int companyId)
    {
        var aircraft = await _aircraftRepository.GetByIdAsync(aircraftDto.Id);
        if (aircraft == null || aircraft.CompanyId != companyId)
        {
            throw new LinkageNotFoundException();
        }
        _mapper.Map(aircraftDto, aircraft);
        await _aircraftRepository.UpdateAsync(aircraft);
    }

    public async Task DeleteAircraftAsync(int id, int companyId)
    {
        var aircraft = await _aircraftRepository.GetByIdAsync(id);
        if (aircraft == null || aircraft.CompanyId != companyId)
        {
            throw new LinkageNotFoundException();
        }
        await _aircraftRepository.DeleteAsync(id);
    }
}
