using Microsoft.EntityFrameworkCore;
using Backend.Persistence.Context;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;

namespace Backend.Persistence.Repositories
{
    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        public AirportRepository(AppDbContext context) : base(context) { }

        public async Task<Airport?> GetAirportByNameAsync(string name)
        {
            return await _context.Airports
                .Where(a => a.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}