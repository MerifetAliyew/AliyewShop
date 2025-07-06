using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.FavouriteDtos;
using AliyewShop.Application.Shared;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouritesController : ControllerBase
{
    private readonly IFavouriteService _favouriteService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FavouritesController(IFavouriteService favouriteService, IHttpContextAccessor httpContextAccessor)
    {
        _favouriteService = favouriteService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public async Task<IActionResult> AddToFavourite(Guid productId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var result = await _favouriteService.AddToFavouriteAsync(userId, new FavouriteCreateDto { ProductId = productId });
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFromFavourite(Guid productId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var result = await _favouriteService.RemoveFromFavouriteAsync(userId, productId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyFavourites()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var result = await _favouriteService.GetMyFavouritesAsync(userId);
        return Ok(result);
    }
}
