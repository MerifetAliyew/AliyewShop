using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;

namespace AliyewShop.Persistence.Repositories;

public class OrderRepository : Repository<Order> ,  IOrderRepository
{
    public OrderRepository(AliyewShopDbContext context) : base(context)
    { 
    }
}
