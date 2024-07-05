using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Persistence.Models;

[Table("aircraft", Schema = "flight_booking_schema")]
public class Aircraft
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Model { get; set; }
    [Required]
    public int Capacity { get; set; }
    [ForeignKey("Company")]
    public int CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    public virtual ICollection<Flight> Flights { get; set; }
    public Aircraft()
    {
        Flights = new HashSet<Flight>();
    }
}