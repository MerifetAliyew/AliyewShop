using AliyewShop.Application.DTOs.RoleDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IRoleService
{
    Task<BaseResponse<string?>> CreateRole(RoleCreateDto dto);
}
