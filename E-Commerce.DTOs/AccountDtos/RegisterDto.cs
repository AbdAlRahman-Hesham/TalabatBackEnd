﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.AccountDtos;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Required]
    [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*])[A-Za-z\\d!@#$%^&*]{7,}$", ErrorMessage = "Password must be at least 7 characters long, include at least one uppercase letter, one lowercase letter, one digit, and one special character (!@#$%^&*).")]
    public string Password { get; set; }
}
