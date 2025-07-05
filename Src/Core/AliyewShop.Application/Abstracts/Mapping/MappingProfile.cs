using AliyewShop.Application.DTOs.OrderDtos;
using AutoMapper;
using AliyewShop.Domain.Entities;


namespace AliyewShop.Application.Abstracts.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Əsas mappinglər:
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
        CreateMap<Order, OrderGetDto>();
    }
}