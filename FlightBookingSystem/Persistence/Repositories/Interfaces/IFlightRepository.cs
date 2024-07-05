using Backend.Api.Models;
using Backend.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories.Interfaces
{
    /// <summary>
    /// Interface for flight repository operations.
    /// </summary>
    public interface IFlightRepository : IRepository<Flight>
    {
        /// <summary>
        /// Searches for flights based on the provided search parameters.
        /// </summary>
        /// <param name="dto">The flight search parameters.</param>
        /// <returns>A collection of flights matching the search criteria.</returns>
        Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchParams dto);
    }
}
