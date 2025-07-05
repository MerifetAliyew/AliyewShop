using System.Linq.Expressions;
using System.Net;
using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.DTOs.FavouriteDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static AliyewShop.Application.Shared.Permissions;

namespace AliyewShop.Persistence.Services;

public class FavouriteService : IFavouriteService
{
    private readonly IFavouriteRepository _favouriteRepository;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository; // Məhsul yoxlamaq üçün (əgər varsa)

    public FavouriteService(
        IFavouriteRepository favouriteRepository,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _favouriteRepository = favouriteRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<BaseResponse<string>> AddAsync(string userId, FavouriteCreateDto dto)
    {
        var productExists = await _productRepository.GetByIdAsync(dto.ProductId);
        if (productExists == null)
        {
            return new BaseResponse<string>("Məhsul tapılmadı", null, HttpStatusCode.NotFound);
        }

        var alreadyExists = await _favouriteRepository.GetByFiltered(f =>
            f.UserId == userId && f.ProductId == dto.ProductId).FirstOrDefaultAsync();

        if (alreadyExists != null)
        {
            return new BaseResponse<string>("Bu məhsul artıq favorilərinizdədir", null, HttpStatusCode.BadRequest);
        }

        var favourite = new Favourite
        {
            UserId = userId,
            ProductId = dto.ProductId
        };

        await _favouriteRepository.AddAsync(favourite);
        await _favouriteRepository.SaveChangeAsync();

        return new BaseResponse<string>("Favoritə əlavə edildi", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var fav = await _favouriteRepository.GetByIdAsync(id);
        if (fav == null)
        {
            return new BaseResponse<string>("Favorit tapılmadı", null, HttpStatusCode.NotFound);
        }

        _favouriteRepository.Delete(fav);
        await _favouriteRepository.SaveChangeAsync();

        return new BaseResponse<string>("Favorit uğurla silindi", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetAllAsync()
    {
        var favorites = await _favouriteRepository.GetAll()
            .Include(f => f.Product)
            .ThenInclude(p => p.Images)
            .ToListAsync();

        if (!favorites.Any())
        {
            return new BaseResponse<List<FavouriteGetDto>>("Favorilər tapılmadı", null, HttpStatusCode.NotFound);
        }

        var dtos = _mapper.Map<List<FavouriteGetDto>>(favorites);
        return new BaseResponse<List<FavouriteGetDto>>("Data", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<FavouriteGetDto>> GetByIdAsync(Guid id)
    {
        var favorite = await _favouriteRepository.GetByIdAsync(id);
        if (favorite is null)
        {
            return new BaseResponse<FavouriteGetDto>("Favori tapılmadı", HttpStatusCode.NotFound);
        }

        var dto = _mapper.Map<FavouriteGetDto>(favorite);
        return new BaseResponse<FavouriteGetDto>("Data", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetByNameAsync(string search)
    {
        return new BaseResponse<List<FavouriteGetDto>>("Ad əsasında axtarış yoxdur", null, HttpStatusCode.BadRequest);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetByNameSearchAsync(string namePart)
    {
        return new BaseResponse<List<FavouriteGetDto>>("Ad ilə axtarış dəstəklənmir", null, HttpStatusCode.BadRequest);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetByUserIdAsync(string userId)
    {
        var favs = await _favouriteRepository
            .GetByFiltered(
                f => f.UserId == userId,
                new Expression<Func<Favourite, object>>[] { f => f.Product }
            )
            .ToListAsync();

        if (!favs.Any())
            return new BaseResponse<List<FavouriteGetDto>>("Favorit tapılmadı", null, HttpStatusCode.NotFound);

        var dtos = _mapper.Map<List<FavouriteGetDto>>(favs);
        return new BaseResponse<List<FavouriteGetDto>>("Favoritlər tapıldı", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> RemoveAsync(string userId, FavouriteRemoveDto dto)
    {
        var favourite = await _favouriteRepository.GetByFiltered(f =>
    f.UserId == userId && f.ProductId == dto.ProductId).FirstOrDefaultAsync();

        if (favourite == null)
        {
            return new BaseResponse<string>("Favorit tapılmadı", null, HttpStatusCode.NotFound);
        }

        _favouriteRepository.Delete(favourite);
        await _favouriteRepository.SaveChangeAsync();

        return new BaseResponse<string>("Favoritdən silindi", HttpStatusCode.OK);
    }
}
