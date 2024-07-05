using Microsoft.AspNetCore.Mvc;
using Backend.Api.Models;
using Backend.Attributes;
using Backend.Services.Interfaces;
using static Backend.Constants.Permissions;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Persistence.Models;

namespace Backend.Api.Controllers;

[ApiController]
[Route("companies")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IUserService _userService;

    public CompanyController(ICompanyService companyService, IUserService userService)
    {
        _companyService = companyService;
        _userService = userService;
    }

    [HttpGet]
    [Permission(Admin, Traveler)]
    public async Task<IActionResult> GetAllCompanies()
    {
        return Ok(await _companyService.GetAllAsync());
    }

    [HttpPost("register")]
    [Permission(CompanyRepresentative)]
    public async Task<IActionResult> RegisterCompany([FromBody] CompanyDTO dto)
    {
        var userId = (await _userService.GetByUsernameAsync(Request.Headers[Constants.Headers.Username]!)).Id!;
        await _companyService.AddCompanyAsync(userId.Value, dto);
        return Ok();
    }

    [HttpGet("pending")]
    [Permission(Admin)]
    public async Task<IActionResult> GetPendingCompanyRegistrations()
    {
        var pendingRegistrations = await _companyService.GetPendingRegistrationRequestsAsync();

        foreach(var pendingRegistration in pendingRegistrations)
        {
            pendingRegistration.Company = await _companyService.GetByIdAsync(pendingRegistration.CompanyId);
            pendingRegistration.User = await _userService.GetByIdAsync(pendingRegistration.UserId);
        }

        return Ok(pendingRegistrations);
    }

    [HttpPost("{companyId}/approve")]
    [Permission(Admin)]
    public async Task<IActionResult> ApproveCompanyRegistration([FromRoute] int companyId)
    {
        await _companyService.ApproveRegistrationRequestAsync(companyId);
        return Ok();
    }

    [HttpPost("{companyId}/reject")]
    [Permission(Admin)]
    public async Task<IActionResult> RejectCompanyRegistration([FromRoute] int companyId)
    {
        await _companyService.RejectRegistrationRequestAsync(companyId);
        return Ok();
    }
}
