namespace AliyewShop.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Fullname { get; set; } = null!;
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Favourite> Favourites { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
