using Backend.Persistence.Models;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories.Interfaces
{
    /// <summary>
    /// Interface for airport repository operations.
    /// </summary>
    public interface IAirportRepository : IRepository<Airport>
    {
        /// <summary>
        /// Retrieves an airport by its name.
        /// </summary>
        /// <param name="name">The name of the airport.</param>
        /// <returns>An airport entity.</returns>
        Task<Airport?> GetAirportByNameAsync(string name);
    }
}
