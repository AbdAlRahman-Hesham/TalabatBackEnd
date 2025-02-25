using E_Commerce.APIs.Middlewares;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.DTOs.BasketDTOs;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.DTOs.OrderDtos;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Identity;
using E_Commerce.Repository.Reprositories;
using E_Commerce.Repository.Reprositories_Interfaces;
using E_Commerce.Repository.UnitOfWork;
using E_Commerce.Services.AuthServices;
using E_Commerce.Services.OrderServices;
using E_Commerce.Services.PaymentServices;
using E_Commerce.Services.ProductServices;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Reflection;
using System.Text;


namespace E_Commerce.APIs.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddContextServices(configuration);
        service.AddApiErrorServices();
        service.AddIdentityServices(configuration);
        service.AddRepositoryServices();
        service.AddScoped<IOrderServices, OrderServices>();
        service.AddScoped<IProductServices, ProductServices>();
        service.AddTransient<IPaymentServices, PaymentServices>();
        service.AddTransient<ExceptionHandlingMiddleware>();
        service.RegisterMapsterConfigurtion();
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

        service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        service.AddScoped<IBasketRepository, BasketRepository>();

        return service;
    }
    private static IServiceCollection AddIdentityServices(this IServiceCollection service, IConfiguration configuration)
    {

        service.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>();

        service.AddScoped<IAuthServices, AuthServices>();

        service.AddAuthentication(op =>
        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!)),
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:Lifetime"]!))

            };
        });
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
    private static IServiceCollection RegisterMapsterConfigurtion(this IServiceCollection service)
    {
        TypeAdapterConfig<OrderDto, Domain.Entities.OrderEntiti.Order>
        .NewConfig()
        .Map(dest => dest.ShippingAddress, src => src.ShippingAddress.Adapt<Domain.Entities.OrderEntities.Address>());

        TypeAdapterConfig<Domain.Entities.OrderEntiti.Order, OrderToReturnDto>
        .NewConfig()
        .Map(des => des.Status, src => src.Status.ToString())
        .Map(des => des.DeliveryMethod, src => src.DeliveryMethod.ShortName)
        .Map(des => des.DeliveryMethodCost, src => src.DeliveryMethod.Cost)
        .Map(des => des.OrderItems, src => src.OrderItems.Adapt<ICollection<OrderItemToReturn>>());


        TypeAdapterConfig<OrderItem, OrderItemToReturn>
            .NewConfig()
            .Map(des => des.ProductId, src => src.Product.ProductId)
            .Map(des => des.ProductName, src => src.Product.ProductName)
            .Map(des => des.PictureUrl, src => src.Product.PictureUrl);
            
        TypeAdapterConfig<UserBasketDto, UserBasket>
         .NewConfig()
         .ConstructUsing(src => new UserBasket(src.Id)
         {
             Items = src.Items.Adapt<List<BasketItem>>() 
         });
        TypeAdapterConfig<BasketItemDto, BasketItem>
            .NewConfig().Map(dest => dest.Id, src => src.Id!.Value);



        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        return service;


    }
    

}
