using AliyewShop.Application.DTOs.OrderDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Services;

public interface IOrderService
{
    Task<BaseResponse<string>> CreateAsync(OrderCreateDto dto, string userId);
    Task<BaseResponse<string>> UpdateAsync(OrderUpdateDto dto, string userId);
    Task<BaseResponse<List<OrderGetDto>>> GetAllAsync();
    Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<OrderGetDto>>> GetMyOrdersAsync(string userId);
    Task<BaseResponse<List<OrderGetDto>>> GetMySalesAsync(string userId);
}
