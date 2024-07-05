using Backend.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories.Interfaces
{
    /// <summary>
    /// Interface for booking repository operations.
    /// </summary>
    public interface IBookingRepository : IRepository<Booking>
    {
        /// <summary>
        /// Retrieves bookings associated with a user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of bookings.</returns>
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);

        /// <summary>
        /// Retrieves bookings associated with a username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A collection of bookings.</returns>
        Task<IEnumerable<Booking>> GetBookingsByUsernameAsync(string username);
    }
}
