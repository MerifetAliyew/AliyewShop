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
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<string>> CreateOrderAsync(string userId, OrderCreateDto dto)
    {
        // Məhsulları bazadan al
        var products = await _productRepository.GetAllByIdsAsync(dto.ProductIds);

        if (products.Count != dto.ProductIds.Count)
            return new BaseResponse<string>("Bəzi məhsullar tapılmadı", HttpStatusCode.BadRequest);

        var orderProducts = products.Select(p => new OrderProduct
        {
            ProductId = p.Id,
            PriceAtOrderTime = p.Price
        }).ToList();

        var grandTotal = orderProducts.Sum(op => op.PriceAtOrderTime);

        var order = new Order
        {
            UserId = userId,
            OrderAt = DateTime.UtcNow,
            PaymentType = dto.PaymentType,
            ShipToAddress = dto.ShipToAddress,
            GrandTotal = grandTotal,
            Progress = "Pending",
            OrderProducts = orderProducts
        };

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangeAsync();

        return new BaseResponse<string>("Sifariş uğurla yaradıldı", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetMyOrdersAsync(string userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        var dtos = _mapper.Map<List<OrderGetDto>>(orders);

        return new BaseResponse<List<OrderGetDto>>("Sizin sifarişləriniz", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetMySalesAsync(string sellerId)
    {
        var orders = await _orderRepository.GetOrdersBySellerIdAsync(sellerId);
        var dtos = _mapper.Map<List<OrderGetDto>>(orders);

        return new BaseResponse<List<OrderGetDto>>("Satışlarınızdakı sifarişlər", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderGetDto>> GetOrderByIdAsync(string userId, Guid orderId)
    {
        var order = await _orderRepository.GetOrderByIdWithDetailsAsync(orderId);

        if (order == null)
            return new BaseResponse<OrderGetDto>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        // Yoxla: sifariş istifadəçiyə və ya məhsul sahibi olan satıcıya aid olmalıdır
        var isOwner = order.UserId == userId;
        var isSeller = order.OrderProducts.Any(op => op.Product.OwnerId == userId);

        if (!isOwner && !isSeller)
            return new BaseResponse<OrderGetDto>("Bu sifarişi görmək icazəniz yoxdur", HttpStatusCode.Forbidden);

        var dto = _mapper.Map<OrderGetDto>(order);
        return new BaseResponse<OrderGetDto>("Sifariş məlumatları", dto, HttpStatusCode.OK);
    }
}
