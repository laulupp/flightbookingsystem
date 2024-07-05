using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models;

public class CompanyRegistrationRequestDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserDTO? User { get; set; }
    public int CompanyId { get; set; }
    public CompanyDTO? Company { get; set; }
    public bool? Status { get; set; }
    [Required]
    public DateTime RequestDate { get; set; }
}
