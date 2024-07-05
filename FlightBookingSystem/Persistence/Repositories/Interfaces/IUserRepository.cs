using Backend.Persistence.Models;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories.Interfaces
{
    /// <summary>
    /// Interface for user repository operations.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A user entity.</returns>
        Task<User?> GetByUsernameAsync(string? username);

        /// <summary>
        /// Retrieves a user by their email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>A user entity.</returns>
        Task<User?> GetByEmailAsync(string? email);

        /// <summary>
        /// Deletes a user and their associated linkages by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        Task DeleteUserAndLinkagesAsync(string? username);
    }
}
