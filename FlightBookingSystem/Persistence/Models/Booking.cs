using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Persistence.Models;

[Table("bookings", Schema = "flight_booking_schema")]
public class Booking
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Flight")]
    public int FlightId { get; set; }
    public virtual Flight? Flight { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    [Required]
    public DateTime BookingDate { get; set; }
}