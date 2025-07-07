using AliyewShop.Application.DTOs.UserDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Repositories;

public interface IRoleRepository
{
    Task<BaseResponse<string>> AddRoleToUserAsync(UserAddRoleDto dto);
}
