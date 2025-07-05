using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Repositories;

public class OrderRepository : Repository<Order> ,  IOrderRepository
{
    private readonly AliyewShopDbContext _context;
    public OrderRepository(AliyewShopDbContext context) : base(context)
    { 
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Order>> GetOrdersForSellerAsync(string sellerId)
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Where(o => o.OrderProducts.Any(op => op.Product.SellerId == sellerId))
            .ToListAsync();
    }
}
