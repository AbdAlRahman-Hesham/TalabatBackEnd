using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Configration;

internal class BrandConfig : IEntityTypeConfiguration<ProductBrand>
{

    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        
        builder.HasData(
            new ProductBrand { Id = 1, Name = "Starbucks" },
            new ProductBrand { Id = 2, Name = "Costa" },
            new ProductBrand { Id = 3, Name = "Cilantro" },
            new ProductBrand { Id = 4, Name = "TBS" },
            new ProductBrand { Id = 5, Name = "On The Run" },
            new ProductBrand { Id = 6, Name = "Caribou" },
            new ProductBrand { Id = 7, Name = "Krispy Kreme" }
        );
    }
}
