using E_Commerce.APIs.Middlewares;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Identity;
using E_Commerce.Repository.Identity.DataSeeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.APIs.Extensions;

public static class MiddelwaresExtension
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
    public static async Task<IApplicationBuilder> UseUpdateDataBase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<StoreContext>();
            await db.Database.MigrateAsync(); 
            
            var dbI = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            await dbI.Database.MigrateAsync();

        }
        return app;
    }
    public static async Task<IApplicationBuilder> UseSeeding(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            await userManager.SeedingUserAsync();
        }
        return app;
    }


}
