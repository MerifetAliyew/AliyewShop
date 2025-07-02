using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;

namespace AliyewShop.Persistence.Repositories;

public class FavouriteRepository : Repository<Favourite> , IFavouriteRepository
{
    public FavouriteRepository(AliyewShopDbContext context) : base(context)
    {
    }
}
