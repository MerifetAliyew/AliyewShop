namespace AliyewShop.Application.DTOs.ImageDtos;

public record class ImageCreateDto
{
    public string ImageUrl { get; set; } = null!;  // Şəkilin URL-i
    public Guid ProductId { get; set; }            // Hansı məhsula aid olduğu
}
