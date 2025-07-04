using AliyewShop.Domain.Entities;

namespace AliyewShop.Application.Abstracts.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetByNameSearchAsync(string namePart);
}
