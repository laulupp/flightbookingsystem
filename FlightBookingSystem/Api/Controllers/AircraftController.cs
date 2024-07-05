using Backend.Api.Models;
using Backend.Attributes;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Backend.Constants.Permissions;

namespace Backend.Api.Controllers;

[ApiController]
[Route("companies/{companyId}/aircrafts")]
public class AircraftController : ControllerBase
{
    private readonly IAircraftService _aircraftService;

    public AircraftController(IAircraftService aircraftService)
    {
        _aircraftService = aircraftService;
    }

    [HttpGet]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> GetAllAircrafts([FromRoute] int companyId)
    {
        var aircrafts = await _aircraftService.GetAllAircraftsAsync(companyId);
        return Ok(aircrafts);
    }

    [HttpGet("{id}")]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> GetAircraftById([FromRoute] int id)
    {
        var aircraft = await _aircraftService.GetAircraftByIdAsync(id);
        return Ok(aircraft);
    }

    [HttpPost]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> AddAircraft([FromRoute] int companyId, [FromBody] AircraftDTO dto)
    {
        var aircraft = await _aircraftService.AddAircraftAsync(dto, companyId);
        return Ok(aircraft);
    }

    [HttpPut("{id}")]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> UpdateAircraft([FromRoute] int id, [FromRoute] int companyId, [FromBody] AircraftDTO dto)
    {
        await _aircraftService.UpdateAircraftAsync(dto, companyId);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> DeleteAircraft([FromRoute] int id, [FromRoute] int companyId)
    {
        await _aircraftService.DeleteAircraftAsync(id, companyId);
        return Ok();
    }
}
