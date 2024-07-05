using Microsoft.AspNetCore.Mvc;
using Backend.Api.Models;
using Backend.Attributes;
using Backend.Services.Interfaces;
using static Backend.Constants.Permissions;

namespace Backend.Api.Controllers;

[ApiController]
[Route("flights")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly IAircraftService _aircraftService;
    private readonly IBookingService _bookingService;

    public FlightController(IFlightService flightService, IAircraftService aircraftService, IBookingService bookingService)
    {
        _flightService = flightService;
        _aircraftService = aircraftService;
        _bookingService = bookingService;
    }

    [HttpGet]
    [Permission(Traveler, Admin)]
    public async Task<IActionResult> GetAvailableFlights([FromQuery] FlightSearchParams dto)
    {
        var flights = await _flightService.SearchFlightsAsync(dto);

        await EnrichWithRemainingTickets(flights);

        return Ok(flights);
    }

    [HttpGet("company/{companyId}")]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> GetCompanyFlights([FromRoute] int companyId)
    {
        var flights = (await _flightService.SearchFlightsAsync(new FlightSearchParams())).Where(c => c.CompanyId == companyId);

        await EnrichWithRemainingTickets(flights);

        return Ok(flights);
    }

    [HttpPost]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> AddFlight([FromBody] FlightDTO dto)
    {
        return Ok(await _flightService.AddFlightAsync(dto));
    }

    [HttpPut("{flightId}")]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> UpdateFlight([FromBody] FlightDTO dto)
    {
        await _flightService.UpdateFlightAsync(dto);
        return Ok();
    }

    [HttpDelete("{flightId}")]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> DeleteFlight([FromRoute] int flightId)
    {
        await _flightService.DeleteFlightAsync(flightId);
        return Ok();
    }

    private async Task EnrichWithRemainingTickets(IEnumerable<FlightDTO> flights)
    {
        foreach (var flight in flights)
        {
            var maxCapacity = (await _aircraftService.GetAircraftByIdAsync(flight.AircraftId!.Value))!.Capacity;

            var bookedTickets = (await _bookingService.GetAllBookingsAsync()).Where(b => b.FlightId == flight.Id).Count();

            flight.RemainingTickets = maxCapacity - bookedTickets;
        }
    }
}