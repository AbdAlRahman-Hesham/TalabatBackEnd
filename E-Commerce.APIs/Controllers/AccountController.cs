using E_Commerce.Domain.Entities.Identity;
using E_Commerce.DTOs.AccountDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.DTOs.ErrorResponse;


namespace E_Commerce.APIs.Controllers;


public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : BaseApiController
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
            return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
        if (!result.Succeeded)
            return Unauthorized(new ApiResponse(401));

        return Ok(new UserDto()
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = "This will be a token"
        });

    }
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,

        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            return BadRequest(new ApiResponse(400));

        return Ok(new UserDto()
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            Token = "temp"
        });




    }
}
