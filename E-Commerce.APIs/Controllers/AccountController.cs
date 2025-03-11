using E_Commerce.Domain.Entities.Identity;
using E_Commerce.DTOs.AccountDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.Domain.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using E_Commerce.APIs.Extensions;
using Mapster;


namespace E_Commerce.APIs.Controllers;


public class AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IAuthServices authServices) : BaseApiController
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly IAuthServices _authServices = authServices;

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
            return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
        if (!result.Succeeded)
            return Unauthorized(new ApiResponse(401));
        var token = await _authServices.CreateToken(user, _userManager);

        return Ok(new UserDto()
        {
            Email = user.Email!,
            DisplayName = user.DisplayName,
            Token = token
        });

    }
    [HttpPost("Register")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(ApiValidationResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {  
        if(CheckEmailExists(registerDto.Email).Result.Value)
            return Conflict(new ApiValidationResponse() { Errors = ["Email is used"],StatusCode = StatusCodes.Status409Conflict });
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
        var token = await _authServices.CreateToken(user, _userManager);

        return Ok(new UserDto()
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            Token = token
        });

    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirst(ClaimTypes.Email)!.Value;
        var user = await _userManager.FindByEmailAsync(email);
        return Ok(new UserDto() {
        Email = user!.Email!,
        DisplayName = user.DisplayName,
        Token = await _authServices.CreateToken(user, _userManager)
        });

    }
    
    [Authorize]
    [HttpGet("address")]
    [ProducesResponseType(typeof(UserAddressDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    public async Task<ActionResult<UserAddressDto>> GetUserAddress()
    {
        var email = User.FindFirst(ClaimTypes.Email)!.Value;
        var user = await _userManager.GetUserWithAddressAsync(email);
        UserAddressDto userAddressDto = user!.Address.Adapt<UserAddressDto>();
        if(userAddressDto is null)
            return BadRequest(new ApiResponse(400,"The user have no address registered"));
        return Ok(userAddressDto);

    }
    
    [HttpPut("address")]
    [ProducesResponseType(typeof(UserAddressDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    public async Task<ActionResult<UserAddressDto>> UpdateUserAddress(UserAddressDto userAddressDto)
    {
        var email = User.FindFirst(ClaimTypes.Email)!.Value;
        
        UserAddressDto? newUserAddressDto = await  _userManager.UpdateUserAddressAsync(email, userAddressDto);

        if (newUserAddressDto is null)
            return BadRequest(new ApiResponse(400,"Error while update user address"));

        return Ok(newUserAddressDto);

    }
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }
}
