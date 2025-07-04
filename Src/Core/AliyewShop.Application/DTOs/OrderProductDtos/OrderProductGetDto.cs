namespace AliyewShop.Application.DTOs.OrderProductDtos;

public record class OrderProductGetDto
{
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductTitle { get; set; } // Məhsulun adı (Product.Title-dan)
    public string? ProductImageUrl { get; set; } // Əgər varsa ilk şəkil URL-i
}