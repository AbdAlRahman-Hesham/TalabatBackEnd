using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Repository.Identity.DataSeeding;

public static class SeedingUsers
{
    public static async Task SeedingUserAsync(this UserManager<AppUser> userManager)
    {
        try
        {
            AppUser appUser = new()
            {
                PhoneNumber = "01205990923",
                Email = "Abdoo@example.com",
                DisplayName = "Abd Al Rahman",
                UserName = "Abdoo"
            };
            await userManager.CreateAsync(appUser, "Pass123!");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error seeding user: {ex.Message}");
            throw;
        }

    }
}
