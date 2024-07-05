using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Persistence.Models;

[Table("flights", Schema = "flight_booking_schema")]
public class Flight
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime DepartureTime { get; set; }
    [Required]
    public DateTime ArrivalTime { get; set; }
    [ForeignKey("Aircraft")]
    public int AircraftId { get; set; }
    public virtual Aircraft? Aircraft { get; set; }
    [ForeignKey("OriginAirport")]
    public int OriginAirportId { get; set; }
    public virtual Airport? OriginAirport { get; set; }
    [ForeignKey("DestinationAirport")]
    public int DestinationAirportId { get; set; }
    public virtual Airport? DestinationAirport { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; }
    public int CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    public Flight()
    {
        Bookings = new HashSet<Booking>();
    }
}