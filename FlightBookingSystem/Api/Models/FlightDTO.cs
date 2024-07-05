using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models;

public class FlightDTO
{
    public int Id { get; set; }
    [Required]
    public DateTime? DepartureTime { get; set; }
    [Required]
    public DateTime? ArrivalTime { get; set; }
    [Required]
    public int? AircraftId { get; set; }
    public AircraftDTO? Aircraft { get; set; }
    [Required]
    public int? OriginAirportId { get; set; }
    public AirportDTO? OriginAirport { get; set; }
    [Required]
    public int? DestinationAirportId { get; set; }
    public AirportDTO? DestinationAirport { get; set; }
    public int CompanyId { get; set; }
    public CompanyDTO? Company { get; set; }
    public int RemainingTickets { get; set; } = 0;
}
