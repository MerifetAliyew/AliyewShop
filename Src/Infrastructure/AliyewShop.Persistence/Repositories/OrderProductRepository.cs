using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Repositories;

public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
{
    private readonly AliyewShopDbContext _context;

    public OrderProductRepository(AliyewShopDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<OrderProduct>> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.OrderProducts
                             .Where(op => op.OrderId == orderId)
                             .Include(op => op.Product)
                             .Include(op => op.Order)
                             .ToListAsync();
    }
}
