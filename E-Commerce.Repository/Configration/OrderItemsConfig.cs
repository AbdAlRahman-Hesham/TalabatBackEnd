using E_Commerce.Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Configration;

internal class OrderItemsConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(builder => builder.Product, io =>
        {
            io.WithOwner();
        });
        builder.Property(i => i.Price).HasColumnType("decimal(18,2)");

    }
}
