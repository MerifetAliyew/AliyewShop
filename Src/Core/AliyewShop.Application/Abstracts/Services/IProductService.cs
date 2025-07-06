using AliyewShop.Application.DTOs.ProductDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IProductService
{
    Task<BaseResponse<List<ProductGetDto>>> GetAllAsync(
        Guid? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string? search = null);

    Task<BaseResponse<ProductGetDto>> GetByIdAsync(Guid id);

    Task<BaseResponse<List<ProductGetDto>>> GetMyProductsAsync(string sellerId);

    Task<BaseResponse<string>> CreateAsync(ProductCreateDto dto, string sellerId);

    Task<BaseResponse<string>> UpdateAsync(ProductUpdateDto dto, string sellerId);

    Task<BaseResponse<string>> DeleteAsync(Guid id, string sellerId);
}
