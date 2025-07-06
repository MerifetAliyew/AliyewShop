using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllWithFiltersAsync(
        Guid? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string? search = null);

    Task<List<Product>> GetBySellerIdAsync(string sellerId);
    Task<List<Product>> GetAllByIdsAsync(List<Guid> ids);
}
