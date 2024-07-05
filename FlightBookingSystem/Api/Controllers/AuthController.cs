using Backend.Api.Models;
using Backend.Persistence.Models;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ICompanyService _companyService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, ICompanyService companyService, IUserService userService)
    {
        _authService = authService;
        _companyService = companyService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var response = await _authService.LoginAsync(dto);

        var userId = (await _userService.GetByUsernameAsync(dto.Username!)).Id;
        if (response.Role == (int)Role.CompanyRepresentative)
        {
            var request = (await _companyService.GetRegistrationsAsync())
               .Where(t => t.UserId == userId)
               .FirstOrDefault();

            if(request == null)
            {
                response.CompanyStatus = 0;
            }
            else if(request.Status == false)
            {
                response.CompanyStatus = 1;
            }
            else
            {
                response.CompanyStatus = 2;
                response.CompanyId = request.CompanyId;
            }
        }

        response.UserId = userId;

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        var response = await _authService.RegisterAsync(dto);

        var userId = (await _userService.GetByUsernameAsync(dto.Username!)).Id;
        response.UserId = userId;

        return Ok(response);
    }
}