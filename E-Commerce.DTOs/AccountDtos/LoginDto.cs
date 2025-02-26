﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.AccountDtos;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
