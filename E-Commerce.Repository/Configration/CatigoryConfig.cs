using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Configration;

internal class CatigoryConfig : IEntityTypeConfiguration<ProductCategory>
{

    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.HasData(
            new ProductCategory { Id = 1, Name = "Frappuccino" },
            new ProductCategory { Id = 2, Name = "Latte" },
            new ProductCategory { Id = 3, Name = "Mocha" },
            new ProductCategory { Id = 4, Name = "Macchiato" },
            new ProductCategory { Id = 5, Name = "Matcha" },
            new ProductCategory { Id = 6, Name = "Cake" },
            new ProductCategory { Id = 7, Name = "Donuts" },
            new ProductCategory { Id = 8, Name = "Salad" }
        );
    }
}
