using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Configration;

internal class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.ShippingAddress, a =>
        {
            a.WithOwner();
        });
        builder.Property(o=>o.Status).HasConversion<string>(
            s=>s.ToString(),
            s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s)
            );
        builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
        builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");

    }
}

