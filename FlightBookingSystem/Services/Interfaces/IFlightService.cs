using Backend.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for flight services.
    /// </summary>
    public interface IFlightService
    {
        /// <summary>
        /// Retrieves all flights.
        /// </summary>
        /// <returns>A collection of flight DTOs.</returns>
        Task<IEnumerable<FlightDTO>> GetAllFlightsAsync();

        /// <summary>
        /// Retrieves a flight by its ID.
        /// </summary>
        /// <param name="flightId">The ID of the flight.</param>
        /// <returns>A flight DTO.</returns>
        Task<FlightDTO?> GetFlightByIdAsync(int flightId);

        /// <summary>
        /// Adds a new flight.
        /// </summary>
        /// <param name="flightDto">The flight data transfer object.</param>
        /// <returns>The added flight DTO.</returns>
        Task<FlightDTO> AddFlightAsync(FlightDTO flightDto);

        /// <summary>
        /// Updates an existing flight.
        /// </summary>
        /// <param name="flightDto">The flight data transfer object.</param>
        Task UpdateFlightAsync(FlightDTO flightDto);

        /// <summary>
        /// Deletes a flight by its ID.
        /// </summary>
        /// <param name="flightId">The ID of the flight.</param>
        Task DeleteFlightAsync(int flightId);

        /// <summary>
        /// Searches for flights based on the provided search parameters.
        /// </summary>
        /// <param name="dto">The flight search parameters.</param>
        /// <returns>A collection of flight DTOs matching the search criteria.</returns>
        Task<IEnumerable<FlightDTO>> SearchFlightsAsync(FlightSearchParams dto);
    }
}
