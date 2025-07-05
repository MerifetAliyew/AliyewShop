using System.Net;
using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.OrderDtos;
using AliyewShop.Application.Shared;
using AutoMapper;
using AliyewShop.Domain.Entities;


namespace AliyewShop.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public  async Task<BaseResponse<string>> CreateAsync(OrderCreateDto dto)
    {
        try
        {
            var orderEntity = _mapper.Map<Order>(dto);
            await _orderRepository.AddAsync(orderEntity);
            await _orderRepository.SaveChangeAsync();

            return new BaseResponse<string>("Sifariş uğurla yaradıldı", HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            var error = ex.InnerException?.Message ?? ex.Message;
            return new BaseResponse<string>($"Xəta baş verdi: {error}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var orderToDelete = await _orderRepository.GetByIdAsync(id);
        if (orderToDelete == null)
            return new BaseResponse<string>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        _orderRepository.Delete(orderToDelete);
        await _orderRepository.SaveChangeAsync();

        return new BaseResponse<string>("Sifariş uğurla silindi", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        if (orders == null || !orders.Any())
            return new BaseResponse<List<OrderGetDto>>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return new BaseResponse<List<OrderGetDto>>("Məlumatlar", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return new BaseResponse<OrderGetDto>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        var dto = _mapper.Map<OrderGetDto>(order);
        return new BaseResponse<OrderGetDto>("Məlumatlar", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetByUserIdAsync(string userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        if (orders == null || !orders.Any())
            return new BaseResponse<List<OrderGetDto>>("İstifadəçiyə aid sifariş tapılmadı", HttpStatusCode.NotFound);

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return new BaseResponse<List<OrderGetDto>>("Məlumatlar", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetMySalesAsync(string sellerId)
    {
        var orders = await _orderRepository.GetOrdersForSellerAsync(sellerId);
        if (orders == null || !orders.Any())
            return new BaseResponse<List<OrderGetDto>>("Satış tapılmadı", HttpStatusCode.NotFound);

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return new BaseResponse<List<OrderGetDto>>("Məlumatlar", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> UpdateAsync(OrderUpdateDto dto)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(dto.Id);
        if (existingOrder == null)
            return new BaseResponse<string>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        _mapper.Map(dto, existingOrder);
        _orderRepository.Update(existingOrder);
        await _orderRepository.SaveChangeAsync();

        return new BaseResponse<string>("Sifariş yeniləndi", HttpStatusCode.OK);
    }
}