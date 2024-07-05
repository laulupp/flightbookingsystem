using Backend.Persistence.Models;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories.Interfaces
{
    /// <summary>
    /// Interface for company repository operations.
    /// </summary>
    public interface ICompanyRepository : IRepository<Company>
    {
        /// <summary>
        /// Retrieves a company by its name.
        /// </summary>
        /// <param name="name">The name of the company.</param>
        /// <returns>A company entity.</returns>
        Task<Company?> GetCompanyByNameAsync(string name);
    }
}
