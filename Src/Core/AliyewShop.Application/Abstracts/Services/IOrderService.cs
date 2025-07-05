using AliyewShop.Application.DTOs.OrderDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Services;

public interface IOrderService
{
    Task<BaseResponse<string>> CreateAsync(OrderCreateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<List<OrderGetDto>>> GetAllAsync();
    Task<BaseResponse<string>> UpdateAsync(OrderUpdateDto dto);
    Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<OrderGetDto>>> GetByUserIdAsync(string userId);
    Task<BaseResponse<List<OrderGetDto>>> GetMySalesAsync(string sellerId);
}

