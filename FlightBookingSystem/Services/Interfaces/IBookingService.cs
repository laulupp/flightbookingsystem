using Backend.Api.Models;

namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for booking services.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Retrieves all bookings.
        /// </summary>
        /// <returns>A collection of booking DTOs.</returns>
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();

        /// <summary>
        /// Retrieves a booking by its ID.
        /// </summary>
        /// <param name="bookingId">The ID of the booking.</param>
        /// <returns>A booking DTO.</returns>
        Task<BookingDTO?> GetBookingByIdAsync(int bookingId);

        /// <summary>
        /// Adds a new booking.
        /// </summary>
        /// <param name="bookingDto">The booking data transfer object.</param>
        /// <returns>The added booking DTO.</returns>
        Task<BookingDTO> AddBookingAsync(BookingDTO bookingDto);

        /// <summary>
        /// Updates an existing booking.
        /// </summary>
        /// <param name="bookingDto">The booking data transfer object.</param>
        Task UpdateBookingAsync(BookingDTO bookingDto);

        /// <summary>
        /// Deletes a booking by its ID.
        /// </summary>
        /// <param name="bookingId">The ID of the booking.</param>
        Task DeleteBookingAsync(int bookingId);

        /// <summary>
        /// Retrieves bookings by username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A collection of booking DTOs.</returns>
        Task<IEnumerable<BookingDTO>> GetBookingsByUsernameAsync(string username);
    }
}
