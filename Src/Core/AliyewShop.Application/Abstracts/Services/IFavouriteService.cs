using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.DTOs.FavouriteDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IFavouriteService
{
    Task<BaseResponse<string>> AddAsync(FavouriteCreateDto dto);

    Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(FavouriteRemoveDto dto);

    Task<BaseResponse<string>> DeleteAsync(Guid id);

    Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id);

    Task<BaseResponse<List<CategoryGetDto>>> GetByNameAsync(string search);

    Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync();
    Task<BaseResponse<List<CategoryGetDto>>> GetByNameSearchAsync(string namePart);
}
