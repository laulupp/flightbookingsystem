using Backend.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for airport services.
    /// </summary>
    public interface IAirportService
    {
        /// <summary>
        /// Retrieves all airports.
        /// </summary>
        /// <returns>A collection of airport DTOs.</returns>
        Task<IEnumerable<AirportDTO>> GetAllAirportsAsync();

        /// <summary>
        /// Retrieves an airport by its ID.
        /// </summary>
        /// <param name="id">The ID of the airport.</param>
        /// <returns>An airport DTO.</returns>
        Task<AirportDTO?> GetAirportByIdAsync(int id);

        /// <summary>
        /// Adds a new airport.
        /// </summary>
        /// <param name="airportDto">The airport data transfer object.</param>
        /// <returns>The added airport DTO.</returns>
        Task<AirportDTO> AddAirportAsync(AirportDTO airportDto);

        /// <summary>
        /// Updates an existing airport.
        /// </summary>
        /// <param name="airportDto">The airport data transfer object.</param>
        Task UpdateAirportAsync(AirportDTO airportDto);

        /// <summary>
        /// Deletes an airport by its ID.
        /// </summary>
        /// <param name="id">The ID of the airport.</param>
        Task DeleteAirportAsync(int id);
    }
}
