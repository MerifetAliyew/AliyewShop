using AliyewShop.Domain.Enums;

namespace AliyewShop.Application.DTOs.UserDtos;

public class UserRegisterDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRoleType Role { get; set; }
}