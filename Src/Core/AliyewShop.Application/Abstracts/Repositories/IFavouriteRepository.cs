using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Repositories;

public interface IFavouriteRepository : IRepository<Favourite>
{
    Task<List<Favourite>> GetFavouritesByUserIdAsync(string userId);
    Task<Favourite?> GetFavouriteByUserIdAndProductIdAsync(string userId, Guid productId);
}
