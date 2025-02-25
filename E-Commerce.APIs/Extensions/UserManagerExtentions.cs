using E_Commerce.Domain.Entities.Identity;
using E_Commerce.DTOs.AccountDtos;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.APIs.Extensions;

public static class UserManagerExtentions
{

    public static async Task<AppUser?> GetUserWithAddressAsync(this UserManager<AppUser> userManager, string email)
    {
        return await userManager.Users.Include(u => u.Address)
            .SingleOrDefaultAsync(u=>u.Email== email);
    }
    public static async Task<UserAddressDto?> UpdateUserAddressAsync(this UserManager<AppUser> userManager,
                                                              string email, UserAddressDto userAddressDto)
    {
        var user = await userManager.Users.Include(e => e.Address)
            .SingleOrDefaultAsync(u=>u.Email==email);
        var userAddress = user!.Address;

        var newUserAddress = userAddressDto.Adapt<UserAddress>();

        if (userAddress is not null)
            newUserAddress.Id = userAddress.Id;

        user!.Address = newUserAddress;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return null;

        return newUserAddress.Adapt<UserAddressDto>();


    }
}
