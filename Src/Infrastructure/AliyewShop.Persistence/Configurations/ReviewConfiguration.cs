using AliyewShop.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.IsConfirmed)
            .IsRequired();

        builder.Property(r => r.ConfirmedAt)
            .IsRequired(false);

        builder.Property(r => r.CommentBody)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        // Review - Product əlaqəsi (1 Product-da çox Review ola bilər)
        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)  // Product entity-də Reviews kolleksiyası olmalıdır
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Cascade silməni qadağan edir

        // Review - User əlaqəsi (1 User-da çox Review ola bilər)
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)  // User entity-də Reviews kolleksiyası olmalıdır
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Cascade silməni qadağan edir
    }
}





