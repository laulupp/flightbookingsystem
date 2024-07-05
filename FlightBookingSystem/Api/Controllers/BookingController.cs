using Microsoft.AspNetCore.Mvc;
using Backend.Api.Models;
using Backend.Attributes;
using Backend.Services.Interfaces;
using static Backend.Constants.Permissions;

namespace Backend.Api.Controllers;

[ApiController]
[Route("bookings")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    [Permission(Traveler)]
    public async Task<IActionResult> GetUserBookings()
    {
        var username = (string)Request.Headers[Constants.Headers.Username]!;
        var bookings = await _bookingService.GetBookingsByUsernameAsync(username);
        return Ok(bookings);
    }

    [HttpPost]
    [Permission(Traveler)]
    public async Task<IActionResult> BookFlight([FromBody] BookingDTO dto)
    {
        await _bookingService.AddBookingAsync(dto);
        return Ok();
    }

    [HttpPut]
    [Permission(Traveler, Admin)]
    public async Task<IActionResult> UpdateBooking([FromBody] BookingDTO dto)
    {
        await _bookingService.UpdateBookingAsync(dto);
        return Ok();
    }

    [HttpDelete("{bookingId}")]
    [Permission(Traveler, Admin)]
    public async Task<IActionResult> CancelBooking([FromRoute] int bookingId)
    {
        await _bookingService.DeleteBookingAsync(bookingId);
        return Ok();
    }
}