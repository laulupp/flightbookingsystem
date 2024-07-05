namespace Backend.Api.Models;

public class FlightSearchParams
{
    public int? OriginAirportId { get; set; }
    public int? DestinationAirportId { get; set; }
    public DateTime? DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public int? CompanyId { get; set; }
}
