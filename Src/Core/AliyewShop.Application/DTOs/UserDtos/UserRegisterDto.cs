

using AliyewShop.Domain.Enum;

namespace AliyewShop.Application.DTOs.UserDtos;

public record class UserRegisterDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; init; }
}