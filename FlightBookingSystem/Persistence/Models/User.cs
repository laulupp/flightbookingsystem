using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Persistence.Models;

[Table("users", Schema = "flight_booking_schema")]
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }
    public Role Role { get; set; }
    [MaxLength(100)]
    [Required]
    public string? Username { get; set; }
    [MaxLength(500)]
    public string? Password { get; set; }
    [MaxLength(100)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(100)]
    [Required]
    public string? Email { get; set; }
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
    public virtual Company? Company { get; set; }
    public virtual CompanyRegistrationRequest? CompanyRegistrationRequest { get; set; }
    public User()
    {
        Bookings = new HashSet<Booking>();
    }
}