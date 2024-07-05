using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models;

public class AircraftDTO
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Model { get; set; }
    [Required]
    public int Capacity { get; set; }
    public int CompanyId { get; set; }
    public CompanyDTO? Company { get; set; }
}
