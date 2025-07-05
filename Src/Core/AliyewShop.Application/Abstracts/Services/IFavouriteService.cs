using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.DTOs.FavouriteDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IFavouriteService
{
    Task<BaseResponse<string>> AddAsync(string userId, FavouriteCreateDto dto);

    Task<BaseResponse<string>> RemoveAsync(string userId, FavouriteRemoveDto dto);

    Task<BaseResponse<string>> DeleteAsync(Guid id);

    Task<BaseResponse<FavouriteGetDto>> GetByIdAsync(Guid id);

    Task<BaseResponse<List<FavouriteGetDto>>> GetByNameAsync(string search);

    Task<BaseResponse<List<FavouriteGetDto>>> GetAllAsync();

    Task<BaseResponse<List<FavouriteGetDto>>> GetByNameSearchAsync(string namePart);

    Task<BaseResponse<List<FavouriteGetDto>>> GetByUserIdAsync(string userId);
}