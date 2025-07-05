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

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    //[Authorize(Policy = Permissions.Order.Create)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
    {
        var response = await _orderService.CreateAsync(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("my")]
    [Authorize(Policy = Permissions.Order.GetMy)]
    [ProducesResponseType(typeof(BaseResponse<List<OrderGetDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new BaseResponse<string>("Token etibarsızdır", HttpStatusCode.Unauthorized));

        var response = await _orderService.GetByUserIdAsync(userId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("my-sales")]
    [Authorize(Policy = Permissions.Order.GetMySales)]
    [ProducesResponseType(typeof(BaseResponse<List<OrderGetDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetMySales()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new BaseResponse<string>("Token etibarsızdır", HttpStatusCode.Unauthorized));

        // Seller üçün ayrıca method (məs: _orderRepository.GetOrdersForSellerAsync(userId)) yazılmalıdır.
        var response = await _orderService.GetMySalesAsync(userId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.Order.GetDetail)]
    [ProducesResponseType(typeof(BaseResponse<OrderGetDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _orderService.GetByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut]
    [Authorize(Policy = Permissions.Order.Update)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] OrderUpdateDto dto)
    {
        var response = await _orderService.UpdateAsync(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Order.Delete)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _orderService.DeleteAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Order.GetAll)]
    [ProducesResponseType(typeof(BaseResponse<List<OrderGetDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _orderService.GetAllAsync();
        return StatusCode((int)response.StatusCode, response);
    }
}
