using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.DTOs.FavouriteDtos;
using AliyewShop.Application.Shared;
using Microsoft.AspNetCore.Http;

namespace AliyewShop.Persistence.Services;

public class FavouriteService : IFavouriteService
{
    public Task<BaseResponse<string>> AddAsync(FavouriteCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<CategoryGetDto>>> GetByNameAsync(string search)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<CategoryGetDto>>> GetByNameSearchAsync(string namePart)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(FavouriteRemoveDto dto)
    {
        throw new NotImplementedException();
    }
}
