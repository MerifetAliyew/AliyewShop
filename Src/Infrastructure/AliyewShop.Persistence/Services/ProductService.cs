using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.ProductDtos;
using AliyewShop.Application.Shared;
using AutoMapper;
using System.Net;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Repositories;

namespace AliyewShop.Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<List<ProductGetDto>>> GetAllAsync(
        Guid? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string? search = null)
    {
        try
        {
            var products = await _productRepository.GetAllWithFiltersAsync(categoryId, minPrice, maxPrice, search);

            var dtos = products.Select(p => _mapper.Map<ProductGetDto>(p)).ToList();

            return new BaseResponse<List<ProductGetDto>>("Məhsullar uğurla gətirildi", dtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<ProductGetDto>>($"Xəta baş verdi: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<ProductGetDto>> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return new BaseResponse<ProductGetDto>("Məhsul tapılmadı", HttpStatusCode.NotFound);

        var dto = _mapper.Map<ProductGetDto>(product);

        return new BaseResponse<ProductGetDto>("Məhsul tapıldı", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ProductGetDto>>> GetMyProductsAsync(string sellerId)
    {
        var products = await _productRepository.GetBySellerIdAsync(sellerId);

        if (products == null || !products.Any())
            return new BaseResponse<List<ProductGetDto>>("Sənin məhsulların tapılmadı", HttpStatusCode.NotFound);

        var dtos = products.Select(p => _mapper.Map<ProductGetDto>(p)).ToList();

        return new BaseResponse<List<ProductGetDto>>("Sənin məhsulların", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> CreateAsync(ProductCreateDto dto, string sellerId)
    {
        try
        {
            var product = _mapper.Map<Product>(dto);
            product.SellerId = sellerId;
            product.OwnerId = sellerId;

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangeAsync();

            return new BaseResponse<string>("Məhsul uğurla əlavə edildi", HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>($"Xəta baş verdi: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<string>> UpdateAsync(ProductUpdateDto dto, string sellerId)
    {
        var existingProduct = await _productRepository.GetByIdAsync(dto.Id);

        if (existingProduct == null)
            return new BaseResponse<string>("Məhsul tapılmadı", HttpStatusCode.NotFound);

        if (existingProduct.OwnerId != sellerId)
            return new BaseResponse<string>("Məhsulu redaktə etmək üçün səlahiyyətiniz yoxdur", HttpStatusCode.Forbidden);

        _mapper.Map(dto, existingProduct);
        existingProduct.OwnerId = sellerId; // Sahibi dəyişmirik əslində, sadəcə təsdiq

        _productRepository.Update(existingProduct);
        await _productRepository.SaveChangeAsync();

        return new BaseResponse<string>("Məhsul uğurla yeniləndi", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id, string sellerId)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct == null)
            return new BaseResponse<string>("Məhsul tapılmadı", HttpStatusCode.NotFound);

        if (existingProduct.OwnerId != sellerId)
            return new BaseResponse<string>("Məhsulu silmək üçün səlahiyyətiniz yoxdur", HttpStatusCode.Forbidden);

        _productRepository.Delete(existingProduct);
        await _productRepository.SaveChangeAsync();

        return new BaseResponse<string>("Məhsul uğurla silindi", HttpStatusCode.OK);
    }
}
