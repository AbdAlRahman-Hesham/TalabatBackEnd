using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Commerce.Repository.Data;

public class StoreContext(DbContextOptions<StoreContext> options):DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }

    public DbSet<ProductCategory> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
