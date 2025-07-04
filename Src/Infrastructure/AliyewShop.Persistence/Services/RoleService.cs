using AliyewShop.Application.DTOs.RoleDtos;
using System.Net;
using System.Security.Claims;
using AliyewShop.Application.Shared;
using AliyewShop.Application.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using AliyewShop.Application.Abstracts.Services;

namespace AliyewShop.Application.Validations.FavouriteValidators;

public class RoleService : IRoleService
{
    private RoleManager<IdentityRole> _roleManager { get; }

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<BaseResponse<string?>> CreateRole(RoleCreateDto dto)
    {
        var existingRole = await _roleManager.FindByNameAsync(dto.Name);
        if (existingRole != null)
        {
            return new BaseResponse<string?>("This role already exists", HttpStatusCode.BadRequest);
        }

        var allPermissions = PermissionHelper.GetPermissionList();
        var invalidPermissions = dto.PermissionsList.Except(allPermissions).ToList();

        if (invalidPermissions.Any())
        {
            return new BaseResponse<string?>(
                $"Invalid permissions: {string.Join(", ", invalidPermissions)}",
                HttpStatusCode.BadRequest
            );
        }

        var newRole = new IdentityRole(dto.Name);
        var result = await _roleManager.CreateAsync(newRole);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return new BaseResponse<string?>(errors, HttpStatusCode.BadRequest);
        }

        foreach (var permission in dto.PermissionsList)
        {
            await _roleManager.AddClaimAsync(newRole, new Claim("Permission", permission));
        }

        return new BaseResponse<string?>("Role created successfully", true, HttpStatusCode.Created);
    }
}

