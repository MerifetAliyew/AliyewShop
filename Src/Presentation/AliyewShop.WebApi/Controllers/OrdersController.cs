using System.Net;
using System.Security.Claims;
using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.OrderDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    // POST /api/orders
    [HttpPost]
    [Authorize(Policy = Permissions.Order.Create)]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

        var result = await _orderService.CreateOrderAsync(userId, dto);

        if (!result.Success)
            return BadRequest(result);

        return StatusCode((int)result.StatusCode, result);
    }

    // GET /api/orders/my
    [HttpGet("my")]
    [Authorize(Policy = Permissions.Order.GetMy)]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

        var result = await _orderService.GetMyOrdersAsync(userId);
        return Ok(result);
    }

    // GET /api/orders/my-sales
    [HttpGet("my-sales")]
    [Authorize(Policy = Permissions.Order.GetMySales)]
    public async Task<IActionResult> GetMySales()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

        var result = await _orderService.GetMySalesAsync(userId);
        return Ok(result);
    }

    // GET /api/orders/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Policy = Permissions.Order.GetDetail)]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

        var result = await _orderService.GetOrderByIdAsync(userId, id);

        if (!result.Success)
        {
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound(result);
            if (result.StatusCode == System.Net.HttpStatusCode.Forbidden)
                return Forbid();
            return BadRequest(result);
        }

        return Ok(result);
    }
}
