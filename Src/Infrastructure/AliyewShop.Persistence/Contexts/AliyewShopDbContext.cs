using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Contexts;

public class AliyewShopDbContext : DbContext
{
    public AliyewShopDbContext(DbContextOptions<AliyewShopDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
    public DbSet<Image> Images { get; set; }

}
