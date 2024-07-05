using Backend.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories.Interfaces
{
    /// <summary>
    /// Interface for company registration request repository operations.
    /// </summary>
    public interface ICompanyRegistrationRequestRepository : IRepository<CompanyRegistrationRequest>
    {
        /// <summary>
        /// Retrieves all pending company registration requests.
        /// </summary>
        /// <returns>A collection of pending registration requests.</returns>
        Task<IEnumerable<CompanyRegistrationRequest>> GetPendingRequestsAsync();

        /// <summary>
        /// Retrieves all company registration requests.
        /// </summary>
        /// <returns>A collection of registration requests.</returns>
        Task<IEnumerable<CompanyRegistrationRequest>> GetRegistrationRequestsAsync();

        /// <summary>
        /// Retrieves all active companies.
        /// </summary>
        /// <returns>A collection of active companies.</returns>
        Task<IEnumerable<Company>> GetActiveCompaniesAsync();

        /// <summary>
        /// Rejects a company registration request by its ID.
        /// </summary>
        /// <param name="requestId">The ID of the registration request.</param>
        Task RejectRequestAsync(int requestId);

        /// <summary>
        /// Approves a company registration request by its ID.
        /// </summary>
        /// <param name="requestId">The ID of the registration request.</param>
        Task ApproveRequestAsync(int requestId);
    }
}
