using AliyewShop.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.StockCount)
            .IsRequired();

        builder.Property(p => p.Size)
            .HasMaxLength(10);

        builder.Property(p => p.Color)
            .HasMaxLength(50);

        builder.Property(p => p.Gender)
            .HasMaxLength(20);

        builder.Property(p => p.Material)
            .HasMaxLength(100);

        builder.Property(p => p.Season)
            .HasMaxLength(50);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Owner)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Favourites)
            .WithOne(f => f.Product)
            .HasForeignKey(f => f.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.Product)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.OrderProducts)
            .WithOne(op => op.Product)
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
