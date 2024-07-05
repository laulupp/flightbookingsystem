using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Services.Interfaces;
using static Backend.Exceptions.CustomExceptions;

namespace Backend.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyRegistrationRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CompanyService(ICompanyRepository companyRepository, ICompanyRegistrationRequestRepository requestRepository, IMapper mapper, ILogger<CompanyService> logger)
    {
        _companyRepository = companyRepository;
        _requestRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CompanyDTO>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all companies");
        var companies = await _companyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CompanyDTO>>(companies);
    }

    public async Task<CompanyDTO?> GetByIdAsync(int companyId)
    {
        _logger.LogInformation("Retrieving company with ID: {CompanyId}", companyId);
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company == null)
        {
            throw new CompanyNotFoundException();
        }
        return _mapper.Map<CompanyDTO>(company);
    }

    public async Task<CompanyDTO> AddCompanyAsync(int userId, CompanyDTO companyDto)
    {
        _logger.LogInformation("Adding new company by user: {UserId}", userId);
        var companyModel = _mapper.Map<Company>(companyDto);
        var company = await _companyRepository.AddAsync(companyModel);
        await _requestRepository.AddAsync(new CompanyRegistrationRequest { CompanyId = company.Id, UserId = userId, RequestDate = DateTime.UtcNow, Status = false });

        return _mapper.Map<CompanyDTO>(company);
    }

    public async Task UpdateCompanyAsync(CompanyDTO companyDto)
    {
        _logger.LogInformation("Updating company with ID: {CompanyId}", companyDto.Id);
        var company = await _companyRepository.GetByIdAsync(companyDto.Id);
        if (company == null)
        {
            throw new CompanyNotFoundException();
        }
        _mapper.Map(companyDto, company);
        await _companyRepository.UpdateAsync(company);
    }

    public async Task DeleteCompanyAsync(int companyId)
    {
        _logger.LogInformation("Deleting company with ID: {CompanyId}", companyId);
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company == null)
        {
            throw new CompanyNotFoundException();
        }
        await _companyRepository.DeleteAsync(companyId);
    }

    public async Task<IEnumerable<CompanyRegistrationRequestDTO>> GetPendingRegistrationRequestsAsync()
    {
        _logger.LogInformation("Retrieving pending company registration requests");
        var requests = await _requestRepository.GetPendingRequestsAsync();
        return _mapper.Map<IEnumerable<CompanyRegistrationRequestDTO>>(requests);
    }

    public async Task<IEnumerable<CompanyRegistrationRequestDTO>> GetRegistrationsAsync()
    {
        _logger.LogInformation("Retrieving all company registration requests");
        var requests = await _requestRepository.GetRegistrationRequestsAsync();
        return _mapper.Map<IEnumerable<CompanyRegistrationRequestDTO>>(requests);
    }

    public async Task ApproveRegistrationRequestAsync(int requestId)
    {
        _logger.LogInformation("Approving registration request with ID: {RequestId}", requestId);
        await _requestRepository.ApproveRequestAsync(requestId);
    }

    public async Task RejectRegistrationRequestAsync(int requestId)
    {
        _logger.LogInformation("Rejecting registration request with ID: {RequestId}", requestId);
        await _requestRepository.RejectRequestAsync(requestId);
    }
}
