namespace AliyewShop.Application.DTOs.ImageDtos;

public class ImageUpdateDto
{
    public Guid Id { get; set; }                   // Dəyişdiriləcək şəklin ID-si
    public string ImageUrl { get; set; } = null!; // Yeni şəkil URL-i
}
