using System.Net;
using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.OrderProductDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Domain.Entities;
using AutoMapper;

namespace AliyewShop.Persistence.Services;

public class OrderProductService : IOrderProductService
{
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IMapper _mapper;

    public OrderProductService(IOrderProductRepository orderProductRepository, IMapper mapper)
    {
        _orderProductRepository = orderProductRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponse<string>> CreateAsync(OrderProductCreateDto dto)
    {
        try
        {
            var orderProduct = _mapper.Map<OrderProduct>(dto);
            await _orderProductRepository.AddAsync(orderProduct);
            await _orderProductRepository.SaveChangeAsync();

            return new BaseResponse<string>("OrderProduct uğurla yaradıldı", HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            var error = ex.InnerException?.Message ?? ex.Message;
            return new BaseResponse<string>($"Xəta baş verdi: {error}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var existing = await _orderProductRepository.GetByIdAsync(id);
        if (existing == null)
            return new BaseResponse<string>("OrderProduct tapılmadı", HttpStatusCode.NotFound);

        _orderProductRepository.Delete(existing);
        await _orderProductRepository.SaveChangeAsync();

        return new BaseResponse<string>("OrderProduct uğurla silindi", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderProductGetDto>> GetByIdAsync(Guid id)
    {
        var orderProduct = await _orderProductRepository.GetByIdAsync(id);
        if (orderProduct == null)
            return new BaseResponse<OrderProductGetDto>("OrderProduct tapılmadı", HttpStatusCode.NotFound);

        var dto = _mapper.Map<OrderProductGetDto>(orderProduct);
        return new BaseResponse<OrderProductGetDto>("Məlumatlar", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderProductGetDto>>> GetByOrderIdAsync(Guid orderId)
    {
        var orderProducts = await _orderProductRepository.GetByOrderIdAsync(orderId);
        if (orderProducts == null || !orderProducts.Any())
            return new BaseResponse<List<OrderProductGetDto>>("OrderProduct tapılmadı", HttpStatusCode.NotFound);

        var dtos = _mapper.Map<List<OrderProductGetDto>>(orderProducts);
        return new BaseResponse<List<OrderProductGetDto>>("Məlumatlar", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> UpdateAsync(OrderProductUpdateDto dto)
    {
        var existing = await _orderProductRepository.GetByIdAsync(dto.Id);
        if (existing == null)
            return new BaseResponse<string>("OrderProduct tapılmadı", HttpStatusCode.NotFound);

        _mapper.Map(dto, existing);
        _orderProductRepository.Update(existing);
        await _orderProductRepository.SaveChangeAsync();

        return new BaseResponse<string>("OrderProduct yeniləndi", HttpStatusCode.OK);
    }
}
