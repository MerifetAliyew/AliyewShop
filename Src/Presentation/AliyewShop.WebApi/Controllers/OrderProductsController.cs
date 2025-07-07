using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.OrderProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderProductsController : ControllerBase
{
    private readonly IOrderProductService _orderProductService;

    public OrderProductsController(IOrderProductService orderProductService)
    {
        _orderProductService = orderProductService;
    }

    [HttpPost]
    [Authorize] 
    public async Task<IActionResult> Create([FromBody] OrderProductCreateDto dto)
    {
        var response = await _orderProductService.CreateAsync(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("orderproduct-refresh")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] OrderProductUpdateDto dto)
    {
        var response = await _orderProductService.UpdateAsync(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _orderProductService.DeleteAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _orderProductService.GetByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("by-order/{orderId}")]
    public async Task<IActionResult> GetByOrderId(Guid orderId)
    {
        var response = await _orderProductService.GetByOrderIdAsync(orderId);
        return StatusCode((int)response.StatusCode, response);
    }
}
