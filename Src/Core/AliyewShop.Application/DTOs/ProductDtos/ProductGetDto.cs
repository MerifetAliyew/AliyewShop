namespace AliyewShop.Application.DTOs.ProductDtos;

public class ProductGetDto
{
    public Guid Id { get; set; }
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
    public string CategoryName { get; set; } = null!;  // Əlavə: kateqoriya adı göstərmək üçün
    public string OwnerId { get; set; } = null!;
    public string OwnerName { get; set; } = null!;     // Satıcının adı və ya username
    public List<string> ImageUrls { get; set; } = new();  // Məhsul şəkilləri
    public double AverageRating { get; set; }  // Məhsulun ortalama reyti
    public int ReviewCount { get; set; }       // Reylərin sayı
}