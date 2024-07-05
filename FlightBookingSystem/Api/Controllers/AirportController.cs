using Backend.Api.Models;
using Backend.Attributes;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Backend.Constants.Permissions;

namespace Backend.Api.Controllers;

[ApiController]
[Route("airports")]
public class AirportController : ControllerBase
{
    private readonly IAirportService _airportService;

    public AirportController(IAirportService airportService)
    {
        _airportService = airportService;
    }

    [HttpGet]
    [Permission(Admin, CompanyRepresentative, Traveler)]
    public async Task<IActionResult> GetAllAirports()
    {
        var airports = await _airportService.GetAllAirportsAsync();
        return Ok(airports);
    }

    [HttpGet("{id}")]
    [Permission(Admin)]
    public async Task<IActionResult> GetAirportById([FromRoute] int id)
    {
        var airport = await _airportService.GetAirportByIdAsync(id);
        return Ok(airport);
    }

    [HttpPost]
    [Permission(Admin)]
    public async Task<IActionResult> AddAirport([FromBody] AirportDTO dto)
    {
        var airport = await _airportService.AddAirportAsync(dto);
        return Ok(airport);
    }

    [HttpPut]
    [Permission(Admin)]
    public async Task<IActionResult> UpdateAirport([FromBody] AirportDTO dto)
    {
        await _airportService.UpdateAirportAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Permission(Admin)]
    public async Task<IActionResult> DeleteAirport([FromRoute] int id)
    {
        await _airportService.DeleteAirportAsync(id);
        return Ok();
    }
}
