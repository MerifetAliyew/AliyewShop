using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;

namespace AliyewShop.Persistence.Repositories;

public class ReviewRepository : Repository<Review> , IReviewRepository
{
    public ReviewRepository(AliyewShopDbContext context) : base(context)
    {
    }
}

