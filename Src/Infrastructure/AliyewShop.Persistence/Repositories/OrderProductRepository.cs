using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;

namespace AliyewShop.Persistence.Repositories;

public  class OrderProductRepository : Repository<OrderProduct> , IOrderProductRepository
{
    public OrderProductRepository(AliyewShopDbContext context) : base(context)
    {
    }
}
