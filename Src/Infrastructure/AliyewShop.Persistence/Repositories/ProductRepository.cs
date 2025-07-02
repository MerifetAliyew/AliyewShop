using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;

namespace AliyewShop.Persistence.Repositories;

public class ProductRepository : Repository<Product> , IProductRepository
{
    public ProductRepository(AliyewShopDbContext context) : base(context)
    {
    }
}
