using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.DTOs.FavouriteDtos;
using AliyewShop.Application.Shared;
using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Services;

public interface IFavouriteService
{
    Task<BaseResponse<string>> AddToFavouriteAsync(string userId, FavouriteCreateDto dto);
    Task<BaseResponse<List<FavouriteGetDto>>> GetMyFavouritesAsync(string userId);
    Task<BaseResponse<string>> RemoveFromFavouriteAsync(string userId, Guid productId);
}