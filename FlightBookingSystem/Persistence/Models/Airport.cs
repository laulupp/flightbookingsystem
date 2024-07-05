using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Persistence.Models;

[Table("airports", Schema = "flight_booking_schema")]
public class Airport
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string? Location { get; set; }
    public virtual ICollection<Flight> OriginFlights { get; set; }
    public virtual ICollection<Flight> DestinationFlights { get; set; }
    public Airport()
    {
        OriginFlights = new HashSet<Flight>();
        DestinationFlights = new HashSet<Flight>();
    }
}