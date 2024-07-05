using Microsoft.EntityFrameworkCore;
using Backend.Persistence.Context;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;

namespace Backend.Persistence.Repositories;

public class CompanyRegistrationRequestRepository : GenericRepository<CompanyRegistrationRequest>, ICompanyRegistrationRequestRepository
{
    public CompanyRegistrationRequestRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<CompanyRegistrationRequest>> GetPendingRequestsAsync()
    {
        return await _context.CompanyRegistrationRequests
            .Where(cr => cr.Status == false)
            .ToListAsync();
    }

    public async Task<IEnumerable<CompanyRegistrationRequest>> GetRegistrationRequestsAsync()
    {
        return await _context.CompanyRegistrationRequests
            .ToListAsync();
    }

    public async Task<IEnumerable<Company>> GetActiveCompaniesAsync()
    {
        return await _context.Companies
            .Include(c => c.CompanyRegistrationRequest)
            .Where(c => c.CompanyRegistrationRequest != null && c.CompanyRegistrationRequest.Status == true)
            .ToListAsync();
    }

    public async Task RejectRequestAsync(int requestId)
    {
        var request = await _context.CompanyRegistrationRequests.FindAsync(requestId);
        if (request != null)
        {
            var companyToBeDeleted = await _context.Companies.FindAsync(request.CompanyId);
            _context.CompanyRegistrationRequests.Remove(request);
            if (companyToBeDeleted != null)
            {
                _context.Companies.Remove(companyToBeDeleted);
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task ApproveRequestAsync(int requestId)
    {
        var request = await _context.CompanyRegistrationRequests.FindAsync(requestId);
        if (request != null)
        {
            request.Status = true;
            _context.CompanyRegistrationRequests.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}