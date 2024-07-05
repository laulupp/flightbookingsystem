using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models;

public class AirportDTO
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Location { get; set; }
}
