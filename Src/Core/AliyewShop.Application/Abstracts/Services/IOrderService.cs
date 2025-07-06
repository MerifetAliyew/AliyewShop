using AliyewShop.Application.DTOs.OrderDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Services;

public interface IOrderService
{
    Task<BaseResponse<string>> CreateOrderAsync(string userId, OrderCreateDto dto);
    Task<BaseResponse<List<OrderGetDto>>> GetMyOrdersAsync(string userId);
    Task<BaseResponse<List<OrderGetDto>>> GetMySalesAsync(string sellerId);
    Task<BaseResponse<OrderGetDto>> GetOrderByIdAsync(string userId, Guid orderId);
}

