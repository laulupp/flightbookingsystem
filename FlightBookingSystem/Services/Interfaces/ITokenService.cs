namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for token services.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates an encrypted token for a given username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The encrypted token.</returns>
        string GenerateEncryptedToken(string username);

        /// <summary>
        /// Validates if a token is valid for a given username.
        /// </summary>
        /// <param name="token">The encrypted token.</param>
        /// <param name="username">The username.</param>
        /// <returns>True if the token is valid, otherwise false.</returns>
        bool IsValidUser(string? token, string? username);
    }
}
