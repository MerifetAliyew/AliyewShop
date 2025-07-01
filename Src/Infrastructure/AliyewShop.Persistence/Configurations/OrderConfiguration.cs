using AliyewShop.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderAt).IsRequired();

        builder.Property(o => o.PaymentType).HasMaxLength(50);

        builder.Property(o => o.ShipToAddress).HasMaxLength(250);

        builder.Property(o => o.GrandTotal).HasColumnType("decimal(18,2)");

        builder.Property(o => o.Progress).HasMaxLength(50);

        builder.Property(o => o.InternalNote).HasMaxLength(500);

        builder.HasMany(o => o.OrderDetails)
               .WithOne(op => op.Order)
               .HasForeignKey(op => op.OrderId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}