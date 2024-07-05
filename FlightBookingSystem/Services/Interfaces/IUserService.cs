using Backend.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for user services.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A user DTO.</returns>
        Task<UserDTO> GetByIdAsync(int userId);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A user DTO.</returns>
        Task<UserDTO> GetByUsernameAsync(string username);

        /// <summary>
        /// Changes the password for a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <param name="ommitOldPasswordCheck">If true, omits the check for the old password.</param>
        Task ChangePasswordAsync(string username, string oldPassword, string newPassword, bool ommitOldPasswordCheck = false);

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="userDto">The user data transfer object.</param>
        /// <param name="contextUsername">The username of the context user.</param>
        /// <param name="ommitUsernameCheck">If true, omits the check for the username.</param>
        Task UpdateInfoAsync(UserDTO userDto, string contextUsername, bool ommitUsernameCheck = false);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A collection of user DTOs.</returns>
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves all company representatives.
        /// </summary>
        /// <returns>A collection of user DTOs representing company representatives.</returns>
        Task<IEnumerable<UserDTO>> GetAllCompaniesAsync();

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>A collection of user DTOs representing customers.</returns>
        Task<IEnumerable<UserDTO>> GetAllCustomersAsync();

        /// <summary>
        /// Deletes a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        Task DeleteByUsernameAsync(string username);

        /// <summary>
        /// Deletes a user and their associated linkages by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        Task DeleteUserAndLinkagesAsync(string? username);
    }
}
