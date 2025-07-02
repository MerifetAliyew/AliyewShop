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

        builder.Property(o => o.PaymentType).HasMaxLength(100).IsRequired(false);
        builder.Property(o => o.ShipToAddress).HasMaxLength(1000).IsRequired(false);
        builder.Property(o => o.GrandTotal).IsRequired(false);
        builder.Property(o => o.Progress).HasMaxLength(50).IsRequired(false);
        builder.Property(o => o.InternalNote).HasMaxLength(1000).IsRequired(false);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}