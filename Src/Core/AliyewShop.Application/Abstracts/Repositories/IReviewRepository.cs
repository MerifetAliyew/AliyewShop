using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetConfirmedReviewsByProductIdAsync(Guid productId);
}