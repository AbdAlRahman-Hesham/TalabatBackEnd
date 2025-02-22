using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Configration;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.Property(p => p.Description).IsRequired();

        builder.Property(p=>p.PictureUrl).IsRequired();

        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId);
        
        builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);

        builder.HasData(
            new Product { Id = 1, Name = "Double Caramel Frappuccino", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit...", Price = 200, PictureUrl = "images/products/sb-ang1.png", CategoryId = 1, BrandId = 1 },
            new Product { Id = 2, Name = "White Chocolate Mocha Frappuccino", Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.", Price = 150, PictureUrl = "images/products/sb-ang2.png", CategoryId = 1, BrandId = 1 },
            new Product { Id = 3, Name = "Iced Cafe Latte", Description = "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", Price = 180, PictureUrl = "images/products/sb-core1.png", CategoryId = 2, BrandId = 1 },
            new Product { Id = 4, Name = "White Chocolate Mocha", Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", Price = 300, PictureUrl = "images/products/sb-core2.png", CategoryId = 3, BrandId = 2 },
            new Product { Id = 5, Name = "Iced Caramel Macchiato", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit...", Price = 250, PictureUrl = "images/products/sb-react1.png", CategoryId = 4, BrandId = 1 },
            new Product { Id = 6, Name = "Hot Caramel Macchiato", Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.", Price = 120, PictureUrl = "images/products/sb-ts1.png", CategoryId = 4, BrandId = 5 },
            new Product { Id = 7, Name = "Iced Matcha Latte", Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero.", Price = 10, PictureUrl = "images/products/hat-core1.png", CategoryId = 5, BrandId = 2 },
            new Product { Id = 8, Name = "Honey Cake", Description = "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", Price = 8, PictureUrl = "images/products/hat-react1.png", CategoryId = 6, BrandId = 1 },
            new Product { Id = 9, Name = "Blueberry Cheesecake", Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero.", Price = 15, PictureUrl = "images/products/hat-react2.png", CategoryId = 6, BrandId = 1 },
            new Product { Id = 10, Name = "Glazed Donuts", Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.", Price = 18, PictureUrl = "images/products/glove-code1.png", CategoryId = 7, BrandId = 3 },
            new Product { Id = 11, Name = "Greek Salad", Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", Price = 15, PictureUrl = "images/products/glove-code2.png", CategoryId = 7, BrandId = 1 },
            new Product { Id = 12, Name = "Iced Black Tea Lemonade", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 16, PictureUrl = "images/products/glove-react1.png", CategoryId = 4, BrandId = 4 },
            new Product { Id = 13, Name = "Iced London Fog Tea Latte", Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", Price = 14, PictureUrl = "images/products/glove-react2.png", CategoryId = 4, BrandId = 4 },
            new Product { Id = 14, Name = "Turkey Bacon, Cheddar & Egg White Sandwich", Description = "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", Price = 250, PictureUrl = "images/products/boot-redis1.png", CategoryId = 3, BrandId = 6 },
            new Product { Id = 15, Name = "Core Red Boots", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 189.99M, PictureUrl = "images/products/boot-core2.png", CategoryId = 3, BrandId = 2 },
            new Product { Id = 16, Name = "Double-Smoked Bacon, Cheddar & Egg Sandwich", Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", Price = 199.99M, PictureUrl = "images/products/boot-core1.png", CategoryId = 3, BrandId = 2 },
            new Product { Id = 17, Name = "Iced Chai Tea Latte with Oleato Golden Foam", Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.", Price = 150, PictureUrl = "images/products/boot-ang2.png", CategoryId = 3, BrandId = 1 },
            new Product { Id = 18, Name = "Dragon Drink® Starbucks Refreshers® Beverage with Oleato Golden Foam", Description = "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", Price = 180, PictureUrl = "images/products/boot-ang1.png", CategoryId = 3, BrandId = 1 }
        );
    }
}
