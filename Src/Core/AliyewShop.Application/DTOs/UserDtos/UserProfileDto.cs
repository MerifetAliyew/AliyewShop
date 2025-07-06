namespace AliyewShop.Application.DTOs.UserDtos;

public record class UserProfileDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
}
