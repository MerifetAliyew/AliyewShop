namespace AliyewShop.Application.DTOs.ProductDtos;

public record class ProductCreateDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockCount { get; set; }
    public string Size { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Material { get; set; } = null!;
    public string Season { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public List<string>? ImageUrls { get; set; } // Optional: bir neçə şəkil URL-si göndərmək üçün
}
