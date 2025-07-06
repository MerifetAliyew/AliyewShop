using AliyewShop.Application.DTOs.OrderProductDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IOrderProductService
{
    Task<BaseResponse<string>> CreateAsync(OrderProductCreateDto dto);
    Task<BaseResponse<string>> UpdateAsync(OrderProductUpdateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<OrderProductGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<OrderProductGetDto>>> GetByOrderIdAsync(Guid orderId);
}