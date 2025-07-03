namespace AliyewShop.Application.DTOs.RoleDtos;

public class RoleCreateDto
{
    public string Name { get; set; } = null!;
    public List<string> PermissionsList { get; set; }
}
