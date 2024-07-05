﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models;

public class AuthResponseDTO
{
    public string? Username { get; set; }
    public string? Token { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Role { get; set; }
    public int CompanyStatus { get; set; } = 0;
    public int? CompanyId { get; set; }
    public int? UserId { get; set; }
}
