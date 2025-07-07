using System.Net;
using AliyewShop.Application.DTOs.RoleDtos;
using AliyewShop.Application.Shared;
using Microsoft.AspNetCore.Identity;

namespace AliyewShop.Application.Abstracts.Services;

public interface IRoleService
{
    Task<BaseResponse<string?>> CreateRole(RoleCreateDto dto);
    Task<BaseResponse<string>> DeleteRoleAsync(string roleName);
    Task<BaseResponse<List<string>>> GetAllRolesAsync();
}
