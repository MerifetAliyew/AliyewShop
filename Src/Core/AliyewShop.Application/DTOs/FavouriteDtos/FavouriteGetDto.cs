namespace AliyewShop.Application.DTOs.FavouriteDtos;

public record class FavouriteGetDto
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
