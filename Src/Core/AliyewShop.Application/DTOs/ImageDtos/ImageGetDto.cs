namespace AliyewShop.Application.DTOs.ImageDtos;

public record  class ImageGetDto
{
    public Guid Id { get; set; }                    // Şəklin ID-si
    public string ImageUrl { get; set; } = null!;  // Şəkilin URL-i
    public Guid ProductId { get; set; }             // Məhsulun ID-si
}