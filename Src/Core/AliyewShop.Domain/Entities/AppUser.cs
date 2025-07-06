using Microsoft.AspNetCore.Identity;

namespace AliyewShop.Domain.Entities;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = null!;
    public string? RefreshToken { get; set; } = null!;
    public DateTime? ExpiryDate { get; set; }
    public ICollection<Product> Products { get; set; } =null!;
    public ICollection<Order> Orders { get; set; } = null!;
    public ICollection<Favourite> Favourites { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
