namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for password encryption services.
    /// </summary>
    public interface IPasswordEncryptionService
    {
        /// <summary>
        /// Encrypts a plain text password.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <returns>The encrypted password.</returns>
        string EncryptPassword(string password);

        /// <summary>
        /// Verifies if a plain text password matches the encrypted password.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <param name="hashedPassword">The encrypted password.</param>
        /// <returns>True if the passwords match, otherwise false.</returns>
        bool VerifyPassword(string password, string hashedPassword);
    }
}
