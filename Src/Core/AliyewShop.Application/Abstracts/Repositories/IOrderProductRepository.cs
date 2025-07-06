using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Repositories;

public interface IOrderProductRepository : IRepository<OrderProduct>
{
    Task<List<OrderProduct>> GetByOrderIdAsync(Guid orderId);
}
