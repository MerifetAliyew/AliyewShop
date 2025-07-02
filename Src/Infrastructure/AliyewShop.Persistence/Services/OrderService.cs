using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.OrderDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Persistence.Services;

public class OrderService : IOrderService
{
    public Task<BaseResponse<string>> CreateAsync(OrderCreateDto dto, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<OrderGetDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<OrderGetDto>>> GetMyOrdersAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<OrderGetDto>>> GetMySalesAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> UpdateAsync(OrderUpdateDto dto, string userId)
    {
        throw new NotImplementedException();
    }
}
