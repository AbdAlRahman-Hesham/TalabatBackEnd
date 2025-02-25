using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.ServicesInterfaces;

public interface IAuthServices
{
    Task<string> CreateToken(AppUser appUser, UserManager<AppUser> userManager);
   


}
