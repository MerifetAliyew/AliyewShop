using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AliyewShopDbContext _context;

    public ProductRepository(AliyewShopDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllWithFiltersAsync(
        Guid? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string? search = null)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .Include(p => p.Images)
            .Include(p => p.Reviews)
            .AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(p =>
                p.Title.ToLower().Contains(search) ||
                p.Description.ToLower().Contains(search));
        }

        return await query.ToListAsync();
    }

    public async Task<List<Product>> GetBySellerIdAsync(string sellerId)
    {
        return await _context.Products
            .Where(p => p.SellerId == sellerId)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .ToListAsync();
    }
    public async Task<List<Product>> GetAllByIdsAsync(List<Guid> ids)
    {
        return await _context.Products
                             .Where(p => ids.Contains(p.Id))
                             .ToListAsync();
    }
}
