using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using AliyewShop.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AliyewShop.Persistence.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AliyewShopDbContext _context;

    public CategoryRepository(AliyewShopDbContext context) : base(context)
    {
        _context = context; 
    }

    public async Task<List<Category>> GetByNameSearchAsync(string namePart)
    {
        return await _context.Categories
            .Where(c => c.Name.Contains(namePart))
            .ToListAsync();
    }
}