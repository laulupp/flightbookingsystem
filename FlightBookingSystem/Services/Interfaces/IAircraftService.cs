using Backend.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for aircraft services.
    /// </summary>
    public interface IAircraftService
    {
        /// <summary>
        /// Retrieves all aircraft for a given company.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>A collection of aircraft DTOs.</returns>
        Task<IEnumerable<AircraftDTO>> GetAllAircraftsAsync(int companyId);

        /// <summary>
        /// Retrieves an aircraft by its ID.
        /// </summary>
        /// <param name="id">The ID of the aircraft.</param>
        /// <returns>An aircraft DTO.</returns>
        Task<AircraftDTO?> GetAircraftByIdAsync(int id);

        /// <summary>
        /// Adds a new aircraft.
        /// </summary>
        /// <param name="aircraftDto">The aircraft data transfer object.</param>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>The added aircraft DTO.</returns>
        Task<AircraftDTO> AddAircraftAsync(AircraftDTO aircraftDto, int companyId);

        /// <summary>
        /// Updates an existing aircraft.
        /// </summary>
        /// <param name="aircraftDto">The aircraft data transfer object.</param>
        /// <param name="companyId">The ID of the company.</param>
        Task UpdateAircraftAsync(AircraftDTO aircraftDto, int companyId);

        /// <summary>
        /// Deletes an aircraft by its ID.
        /// </summary>
        /// <param name="id">The ID of the aircraft.</param>
        /// <param name="companyId">The ID of the company.</param>
        Task DeleteAircraftAsync(int id, int companyId);
    }
}
