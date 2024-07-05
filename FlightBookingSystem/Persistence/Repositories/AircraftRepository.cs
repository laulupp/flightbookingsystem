using Microsoft.EntityFrameworkCore;
using Backend.Persistence.Context;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;

namespace Backend.Persistence.Repositories;

public class AircraftRepository : GenericRepository<Aircraft>, IAircraftRepository
{
    public AircraftRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Aircraft>> GetAircraftsByCompanyIdAsync(int companyId)
    {
        return await _context.Aircraft
            .Where(a => a.CompanyId == companyId)
            .ToListAsync();
    }
}