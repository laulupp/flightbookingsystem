using Backend.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    /// <summary>
    /// Interface for company services.
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>A collection of company DTOs.</returns>
        Task<IEnumerable<CompanyDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a company by its ID.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>A company DTO.</returns>
        Task<CompanyDTO?> GetByIdAsync(int companyId);

        /// <summary>
        /// Adds a new company.
        /// </summary>
        /// <param name="userId">The ID of the user registering the company.</param>
        /// <param name="companyDto">The company data transfer object.</param>
        /// <returns>The added company DTO.</returns>
        Task<CompanyDTO> AddCompanyAsync(int userId, CompanyDTO companyDto);

        /// <summary>
        /// Updates an existing company.
        /// </summary>
        /// <param name="companyDto">The company data transfer object.</param>
        Task UpdateCompanyAsync(CompanyDTO companyDto);

        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        Task DeleteCompanyAsync(int companyId);

        /// <summary>
        /// Retrieves pending company registration requests.
        /// </summary>
        /// <returns>A collection of company registration request DTOs.</returns>
        Task<IEnumerable<CompanyRegistrationRequestDTO>> GetPendingRegistrationRequestsAsync();

        /// <summary>
        /// Retrieves all company registration requests.
        /// </summary>
        /// <returns>A collection of company registration request DTOs.</returns>
        Task<IEnumerable<CompanyRegistrationRequestDTO>> GetRegistrationsAsync();

        /// <summary>
        /// Approves a company registration request.
        /// </summary>
        /// <param name="requestId">The ID of the registration request.</param>
        Task ApproveRegistrationRequestAsync(int requestId);

        /// <summary>
        /// Rejects a company registration request.
        /// </summary>
        /// <param name="requestId">The ID of the registration request.</param>
        Task RejectRegistrationRequestAsync(int requestId);
    }
}
