using System.Net;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.RoleDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Application.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost("create-role")]
    [ProducesResponseType(typeof(BaseResponse<string?>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<string?>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string?>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto dto)
    {
        var result = await _roleService.CreateRole(dto);
        return StatusCode((int)result.StatusCode, result);
    }


    [HttpGet("permissions")]
    public IActionResult GetAllPermissions()
    {
        var permissions = PermissionHelper.GetAllPermissions();
        return Ok(permissions);
    }

    [HttpDelete("{roleName}")]
    [Authorize(Policy = Permissions.Role.Delete)]
    public async Task<IActionResult> DeleteRole(string roleName)
    {
        var response = await _roleService.DeleteRoleAsync(roleName);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await _roleService.GetAllRolesAsync();

        if (response.StatusCode == HttpStatusCode.OK)
            return Ok(response.Data);

        return StatusCode((int)response.StatusCode, response.Message);
    }

}
