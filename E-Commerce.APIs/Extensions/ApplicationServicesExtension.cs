using E_Commerce.APIs.Middlewares;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Identity;
using E_Commerce.Repository.Reprositories;
using E_Commerce.Repository.Reprositories_Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;


namespace E_Commerce.APIs.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddContextServices(configuration);
        service.AddApiErrorServices();
        service.AddIdentityServices();
        service.AddRepositoryServices();
        service.AddTransient<ExceptionHandlingMiddleware>();



        return service;
    }

    private static IServiceCollection AddContextServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<StoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("TalabatDb")));
        service.AddDbContext<IdentityContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("TalabatDbIdentity")));
        service.AddSingleton<IConnectionMultiplexer>(
            op =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            }
            );
        return service;
    }
    private static IServiceCollection AddRepositoryServices(this IServiceCollection service)
    {

        service.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
        service.AddScoped<IBasketRepository, BasketRepository>();

        return service;
    }
    private static IServiceCollection AddIdentityServices(this IServiceCollection service)
    {

        service.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>();

        return service;
    }
    private static IServiceCollection AddApiErrorServices(this IServiceCollection service)
    {

        service.Configure<ApiBehaviorOptions>(
            op =>
            {
                op.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value!.Errors.Count > 0)
                                                          .SelectMany(x => x.Value!.Errors)
                                                          .Select(x => x.ErrorMessage).ToList();

                    var validationErrorResponse = new ApiValidationResponse() { Errors = errors };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            }

            );

        return service;
    }
}
