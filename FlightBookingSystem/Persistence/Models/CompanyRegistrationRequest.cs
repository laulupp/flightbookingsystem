using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Persistence.Models;

[Table("company_registration_requests", Schema = "flight_booking_schema")]
public class CompanyRegistrationRequest
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    [ForeignKey("Company")]
    public int CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    [Required]
    public bool? Status { get; set; }
    [Required]
    public DateTime RequestDate { get; set; }
}