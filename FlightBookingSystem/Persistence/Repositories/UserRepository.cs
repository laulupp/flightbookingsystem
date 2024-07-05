using Microsoft.EntityFrameworkCore;
using Backend.Persistence.Context;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;

namespace Backend.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByUsernameAsync(string? username)
    {
        return await _context.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string? email)
    {
        return await _context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteUserAndLinkagesAsync(string? username)
    {

        var user = await GetByUsernameAsync(username);
        if (user == null)
        {
            return;
        }

        var userId = user.Id;

        var crrIds  = await _context.CompanyRegistrationRequests.Where(crr => crr.UserId == userId)
                                                                .Select(crr => crr.Id)
                                                                .ToListAsync();

        var companyIds = await _context.CompanyRegistrationRequests.Where(crr => crr.UserId == userId)
                                                                   .Select(crr => crr.CompanyId)
                                                                   .ToListAsync();

        var aircraftIds = await _context.Aircraft.Where(a => companyIds != null && companyIds.Contains(a.CompanyId))
                                                 .Select(a => a.Id)               
                                                 .ToListAsync();

        var flightIds = await _context.Flights.Where(f => companyIds != null && companyIds.Contains(f.CompanyId))
                                              .Select(a => a.Id)
                                              .ToListAsync();

        var bookingIds = await _context.Bookings
                                      .Where(b => b.UserId == userId || flightIds != null && flightIds.Contains(b.FlightId))
                                      .Select(b => b.Id)
                                      .ToListAsync();

        foreach(var b in bookingIds)
        {
            var entity = await _context.Bookings.FindAsync(b);
            if (entity != null)
            {
                _context.Bookings.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        foreach(var f in flightIds)
        {
            var entity = await _context.Flights.FindAsync(f);
            if (entity != null)
            {
                _context.Flights.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        
        foreach(var a in aircraftIds)
        {
            var entity = await _context.Aircraft.FindAsync(a);
            if (entity != null)
            {
                _context.Aircraft.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        foreach(var crr in crrIds)
        {
            var entity = await _context.CompanyRegistrationRequests.FindAsync(crr);
            if (entity != null)
            {
                _context.CompanyRegistrationRequests.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        await DeleteAsync(user.Id);
    }
}