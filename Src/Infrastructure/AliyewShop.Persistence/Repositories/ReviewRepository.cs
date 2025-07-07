using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    private readonly AliyewShopDbContext _context;

    public ReviewRepository(AliyewShopDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetConfirmedReviewsByProductIdAsync(Guid productId)
    {
        return await _context.Reviews
            .Where(r => r.ProductId == productId && r.IsConfirmed)
            .Include(r => r.User)  
            .ToListAsync();
    }
}
