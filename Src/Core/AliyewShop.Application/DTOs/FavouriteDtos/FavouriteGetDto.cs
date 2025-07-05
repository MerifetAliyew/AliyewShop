namespace AliyewShop.Application.DTOs.FavouriteDtos;

public record class FavouriteGetDto
{
    public Guid Id { get; set; } // Favoritin öz ID-si
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public string? ProductImageUrl { get; set; } = null;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
