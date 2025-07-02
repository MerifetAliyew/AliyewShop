using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;

namespace AliyewShop.Persistence.Repositories;

public class CategoryRepository : Repository<Category> , ICategoryRepository
{
    public CategoryRepository(AliyewShopDbContext context) : base(context)
    {
    }
}
