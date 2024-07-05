using Microsoft.AspNetCore.Mvc;
using Backend.Api.Models;
using Backend.Attributes;
using Backend.Services.Interfaces;
using static Backend.Constants.Permissions;

namespace Backend.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Permission(Admin)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("profile")]
    [Permission(Traveler, CompanyRepresentative, Admin)]
    public async Task<IActionResult> GetUserProfile()
    {
        var username = Request.Headers[Constants.Headers.Username].ToString();
        var user = await _userService.GetByUsernameAsync(username);
        return Ok(user);
    }

    [HttpPut]
    [Permission(Traveler, CompanyRepresentative, Admin)]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UserDTO dto)
    {
        var username = Request.Headers[Constants.Headers.Username].ToString();
        var isAdmin = (bool?)Request.HttpContext.Items[Constants.Items.IsAdmin];

        if (isAdmin != null && isAdmin.Value)
        {
            await _userService.UpdateInfoAsync(dto, username, true);
        }
        else
        {
            await _userService.UpdateInfoAsync(dto, username);
        }
        return Ok();
    }

    [HttpPut("changepassword")]
    [Permission(Traveler, CompanyRepresentative, Admin)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
    {
        var username = Request.Headers[Constants.Headers.Username].ToString();
        var isAdmin = (bool?)Request.HttpContext.Items[Constants.Items.IsAdmin];

        if (isAdmin != null && isAdmin.Value)
        {
            await _userService.ChangePasswordAsync(username, dto.OldPassword, dto.NewPassword!, true);
        }
        else
        {
            await _userService.ChangePasswordAsync(username, dto.OldPassword, dto.NewPassword!);
        }
        return Ok();
    }

    [HttpDelete]
    [Permission(Admin)]
    public async Task<IActionResult> DeleteUser([FromQuery] string username)
    {
        await _userService.DeleteByUsernameAsync(username);
        return Ok();
    }
}