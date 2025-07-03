using System.Net;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.UserDtos;
using AliyewShop.Application.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private IUserService _userService { get; }
    public AccountsController(IUserService userService)
    {
        _userService = userService;

    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await _userService.Register(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var result = await _userService.Login(dto);
        return StatusCode((int)result.StatusCode, result);
    }
}
