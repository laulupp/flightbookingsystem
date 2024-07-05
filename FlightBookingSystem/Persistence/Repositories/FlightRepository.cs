using Microsoft.EntityFrameworkCore;
using Backend.Persistence.Context;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Api.Models;

namespace Backend.Persistence.Repositories;

public class FlightRepository : GenericRepository<Flight>, IFlightRepository
{
    public FlightRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchParams dto)
    {
        var query = _context.Flights.AsQueryable();

        if (dto.OriginAirportId != null)
        {
            query = query.Where(f => f.OriginAirportId == dto.OriginAirportId);
        }

        if (dto.DestinationAirportId != null)
        {
            query = query.Where(f => f.DestinationAirportId == dto.DestinationAirportId);
        }

        if (dto.DepartureTime != null)
        {
            query = query.Where(f => f.DepartureTime >= dto.DepartureTime);
        }

        if (dto.ArrivalTime != null)
        {
            query = query.Where(f => f.ArrivalTime <= dto.ArrivalTime);
        }

        if (dto.CompanyId != null)
        {
            query = query.Where(f => f.CompanyId == dto.CompanyId);
        }

        return await query.ToListAsync();
    }

}