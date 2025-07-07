using AliyewShop.Application.DTOs.OrderDtos;
using AutoMapper;
using AliyewShop.Domain.Entities;
using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.DTOs.OrderProductDtos;
using AliyewShop.Application.DTOs.ProductDtos;
using AliyewShop.Application.DTOs.FavouriteDtos;


namespace AliyewShop.Application.Abstracts.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category mappinglər:
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryGetDto>();
        CreateMap<Category, CategoryUpdateDto>();
        CreateMap<Category, CategoryTreeDto>();
        // OrderProduct ↔ DTOs
        CreateMap<OrderProductCreateDto, OrderProduct>();
        CreateMap<OrderProductUpdateDto, OrderProduct>();
        CreateMap<OrderProduct, OrderProductGetDto>()
            .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Order.Id.ToString()));
        //Product         CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        CreateMap<Product, ProductGetDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.UserName))
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0))
            .ForMember(dest => dest.ReviewCount, opt => opt.MapFrom(src => src.Reviews.Count));

        //Faavourite
        CreateMap<Favourite, FavouriteGetDto>()
            .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Product.Images.Select(i => i.ImageUrl)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        // Order
        // Order -> OrderGetDto
        CreateMap<Order, OrderGetDto>()
    .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));

        CreateMap<OrderProduct, OrderProductDto>()
            .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice));

        // OrderProduct -> OrderProductGetDto
        CreateMap<OrderProduct, OrderProductGetDto>()
            .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title));

        // OrderCreateDto -> Order (əslində bu mapping service-də tam lazım deyil, çünki
        // ProductIds əllə işlənir, ona görə burada skip edirik)
        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.OrderProducts, opt => opt.Ignore());

        //Favourite 
        CreateMap<FavouriteCreateDto, Favourite>();
    }
}