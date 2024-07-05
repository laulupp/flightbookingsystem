using Backend.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories.Interfaces
{
    /// <summary>
    /// Interface for aircraft repository operations.
    /// </summary>
    public interface IAircraftRepository : IRepository<Aircraft>
    {
        /// <summary>
        /// Retrieves all aircrafts associated with a given company ID.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>A collection of aircrafts.</returns>
        Task<IEnumerable<Aircraft>> GetAircraftsByCompanyIdAsync(int companyId);
    }
}
