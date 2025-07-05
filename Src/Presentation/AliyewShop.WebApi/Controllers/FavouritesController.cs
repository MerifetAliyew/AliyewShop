using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.FavouriteDtos;
using AliyewShop.Application.Shared;
using System.Net;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouritesController : ControllerBase
{
    private readonly IFavouriteService _favouriteService;

    public FavouritesController(IFavouriteService favouriteService)
    {
        _favouriteService = favouriteService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddFavourite([FromQuery] string userId, [FromBody] FavouriteCreateDto dto)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new BaseResponse<string>("UserId boş ola bilməz", null, HttpStatusCode.BadRequest));

        var response = await _favouriteService.AddAsync(userId, dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteFavourite(Guid id)
    {
        var response = await _favouriteService.DeleteAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllFavourites()
    {
        var response = await _favouriteService.GetAllAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFavouriteById(Guid id)
    {
        var response = await _favouriteService.GetByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetFavouritesByUser([FromQuery] string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new BaseResponse<string>("UserId boş ola bilməz", null, HttpStatusCode.BadRequest));

        var response = await _favouriteService.GetByUserIdAsync(userId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveFavourite([FromQuery] string userId, [FromBody] FavouriteRemoveDto dto)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new BaseResponse<string>("UserId boş ola bilməz", null, HttpStatusCode.BadRequest));

        var response = await _favouriteService.RemoveAsync(userId, dto);
        return StatusCode((int)response.StatusCode, response);
    }
}
