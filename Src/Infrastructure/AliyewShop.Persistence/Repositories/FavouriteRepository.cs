using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using AliyewShop.Domain.Entities;

namespace AliyewShop.Persistence.Repositories;

public class FavouriteRepository : Repository<Favourite>, IFavouriteRepository
{
    private readonly AliyewShopDbContext _context;

    public FavouriteRepository(AliyewShopDbContext context) : base(context)
    {
        _context = context;
    }
    public void Remove(Favourite entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        _context.Favourites.Update(entity);
    }

    public async Task<List<Favourite>> GetFavouritesByUserIdAsync(string userId)
    {
        return await _context.Favourites
            .Include(f => f.Product)
                .ThenInclude(p => p.Images)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<Favourite?> GetFavouriteByUserIdAndProductIdAsync(string userId, Guid productId)
    {
        return await _context.Favourites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
    }

    public async Task<Favourite?> GetByUserIdAndProductIdAsync(string userId, Guid productId)
    {
        return await _context.Favourites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
    }
}
