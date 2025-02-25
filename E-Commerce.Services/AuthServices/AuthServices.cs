﻿using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Services.AuthServices;

public class AuthServices(IConfiguration configuration) : IAuthServices
{
    private readonly IConfiguration configuration = configuration;

    public async Task<string> CreateToken(AppUser appUser, UserManager<AppUser> userManager)
    {
        List<Claim> claims = new List<Claim> {
              new Claim(ClaimTypes.Email, appUser.Email!),
              new Claim(ClaimTypes.Name, appUser.DisplayName),
              new Claim(ClaimTypes.MobilePhone, appUser.PhoneNumber!)
        };
        var roleClaims = await userManager.GetClaimsAsync(appUser);
        claims.AddRange(roleClaims);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(double.Parse(configuration["JWT:Lifetime"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);


    }
}
