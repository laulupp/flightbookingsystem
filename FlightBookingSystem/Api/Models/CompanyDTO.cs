using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models;

public class CompanyDTO
{
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string? Name { get; set; }
    [Required]
    public string? RegistrationDetails { get; set; }
    public int UserId { get; set; }
    public UserDTO? User { get; set; }
}
