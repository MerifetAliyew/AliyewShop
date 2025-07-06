using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    Task<List<Order>> GetOrdersBySellerIdAsync(string sellerId);
    Task<Order?> GetOrderByIdWithDetailsAsync(Guid id);
}
