using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;

namespace AliyewShop.Persistence.Repositories;

public class ImageRepository : Repository<Image> , IImageRepository
{
    public ImageRepository(AliyewShopDbContext context) : base(context)
    {
    }
}
