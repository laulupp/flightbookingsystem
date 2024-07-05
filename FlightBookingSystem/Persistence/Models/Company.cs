using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Persistence.Models;

[Table("companies", Schema = "flight_booking_schema")]
public class Company
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string? Name { get; set; }
    [Required]
    public string? RegistrationDetails { get; set; }
    public virtual ICollection<Aircraft> Aircrafts { get; set; }
    public virtual ICollection<Flight> Flights { get; set; }
    public virtual CompanyRegistrationRequest? CompanyRegistrationRequest { get; set; }
    public Company()
    {
        Aircrafts = new HashSet<Aircraft>();
        Flights = new HashSet<Flight>();
    }
}