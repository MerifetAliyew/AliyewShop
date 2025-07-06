using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductsController(IProductService productService, IHttpContextAccessor httpContextAccessor)
    {
        _productService = productService;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET /api/products?categoryId=&minPrice=&maxPrice=&search=
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? categoryId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] string? search)
    {
        var result = await _productService.GetAllAsync(categoryId, minPrice, maxPrice, search);
        return StatusCode((int)result.StatusCode, result);
    }

    // GET /api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    // GET /api/products/my
    [HttpGet("my")]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> GetMyProducts()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        var result = await _productService.GetMyProductsAsync(userId);
        return StatusCode((int)result.StatusCode, result);
    }

    // POST /api/products
    [HttpPost]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        var result = await _productService.CreateAsync(dto, userId);
        return StatusCode((int)result.StatusCode, result);
    }

    // PUT /api/products/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID uyğun gəlmir");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        var result = await _productService.UpdateAsync(dto, userId);
        return StatusCode((int)result.StatusCode, result);
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        var result = await _productService.DeleteAsync(id, userId);
        return StatusCode((int)result.StatusCode, result);
    }
}
