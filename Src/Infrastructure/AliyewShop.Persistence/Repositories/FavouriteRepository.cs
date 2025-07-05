using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Repositories;

public class FavouriteRepository : Repository<Favourite>, IFavouriteRepository
{
    private readonly AliyewShopDbContext _context;
    public FavouriteRepository(AliyewShopDbContext context) : base(context)
    {
    }

    public async Task<List<Favourite>> GetByAdIdAsync(Guid adId)
    {
        return await _context.Set<Favourite>()
            .Where(f => f.ProductId == adId)
            .Include(f => f.Product)
            .ToListAsync();
    }
}
