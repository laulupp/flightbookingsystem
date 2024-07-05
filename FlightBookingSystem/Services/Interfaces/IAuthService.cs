using Backend.Api.Models;

namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for authentication services.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="loginDTO">The login data transfer object containing username and password.</param>
        /// <returns>An authentication response DTO with user details and a token.</returns>
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDTO">The register data transfer object containing user details.</param>
        /// <returns>An authentication response DTO with user details and a token.</returns>
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);
    }
}
