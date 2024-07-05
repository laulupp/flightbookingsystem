using Microsoft.EntityFrameworkCore;
using Backend.Persistence.Context;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;

namespace Backend.Persistence.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context) { }

        public async Task<Company?> GetCompanyByNameAsync(string name)
        {
            return await _context.Companies
                .Where(c => c.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}