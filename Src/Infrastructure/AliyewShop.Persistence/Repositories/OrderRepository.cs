using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly AliyewShopDbContext _context;

    public OrderRepository(AliyewShopDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Order>> GetOrdersBySellerIdAsync(string sellerId)
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Where(o => o.OrderProducts.Any(op => op.Product.OwnerId == sellerId))
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdWithDetailsAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
