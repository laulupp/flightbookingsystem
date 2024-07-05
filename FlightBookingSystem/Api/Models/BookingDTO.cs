using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models;

public class BookingDTO
{
    public int Id { get; set; }
    [Required]
    public int? UserId { get; set; }
    public UserDTO? User { get; set; }
    [Required]
    public int? FlightId { get; set; }
    public FlightDTO? Flight { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public string? SeatNumber { get; set; }
}
