namespace AliyewShop.Application.DTOs.ImageDtos;

public class ImageCreateDto
{
    public string ImageUrl { get; set; } = null!;  // Şəkilin URL-i
    public Guid ProductId { get; set; }            // Hansı məhsula aid olduğu
}
