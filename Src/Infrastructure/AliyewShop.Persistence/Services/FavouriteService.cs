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
using AliyewShop.Domain.Entities;

namespace AliyewShop.Persistence.Services;

public class FavouriteService : IFavouriteService
{
    private readonly IFavouriteRepository _favouriteRepository;
    private readonly IMapper _mapper;

    public FavouriteService(IFavouriteRepository favouriteRepository, IMapper mapper)
    {
        _favouriteRepository = favouriteRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<string>> AddToFavouriteAsync(string userId, FavouriteCreateDto dto)
    {
        var favourite = _mapper.Map<Favourite>(dto);
        favourite.UserId = userId;

        await _favouriteRepository.AddAsync(favourite);
        await _favouriteRepository.SaveChangeAsync();

        return new BaseResponse<string>("Məhsul favoritlərə əlavə edildi", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetMyFavouritesAsync(string userId)
    {
        var favourites = await _favouriteRepository.GetFavouritesByUserIdAsync(userId);
        var dtos = _mapper.Map<List<FavouriteGetDto>>(favourites);

        return new BaseResponse<List<FavouriteGetDto>>("Sevimlilər siyahısı", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> RemoveFromFavouriteAsync(string userId, Guid productId)
    {
        var favourite = await _favouriteRepository.GetFavouriteByUserIdAndProductIdAsync(userId, productId);
        if (favourite == null)
            return new BaseResponse<string>("Sevimli məhsul tapılmadı", HttpStatusCode.NotFound);

        _favouriteRepository.Delete(favourite);
        await _favouriteRepository.SaveChangeAsync();

        return new BaseResponse<string>("Sevimli məhsul silindi", HttpStatusCode.OK);
    }
}
