namespace AliyewShop.Domain.Entities;

public class Favourite : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
