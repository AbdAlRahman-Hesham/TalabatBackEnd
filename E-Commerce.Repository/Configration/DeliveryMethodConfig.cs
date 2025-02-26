﻿using E_Commerce.Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Configration;

internal class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(d => d.Cost).HasColumnType("decimal(18,2)");
        builder.HasData(
            new DeliveryMethod { Id = 1, ShortName = "UPS1", Description = "Fastest delivery time", DeliveryTime = "1-2 Days", Cost = 10m },
            new DeliveryMethod { Id = 2, ShortName = "UPS2", Description = "Get it within 5 days", DeliveryTime = "2-5 Days", Cost = 5m },
            new DeliveryMethod { Id = 3, ShortName = "UPS3", Description = "Slower but cheap", DeliveryTime = "5-10 Days", Cost = 2m },
            new DeliveryMethod { Id = 4, ShortName = "FREE", Description = "Free! You get what you pay for", DeliveryTime = "1-2 Weeks", Cost = 0m }
        );
    }
}

