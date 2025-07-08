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
            .Where(c => !c.IsDeleted && c.Name.Contains(namePart))
            .ToListAsync();
    }

    public async Task<List<Category>> GetAllWithSubCategoriesAsync()
    {
        return await _context.Categories
            .Where(c => !c.IsDeleted)
            .Include(c => c.SubCategories.Where(sc => !sc.IsDeleted))
            .ToListAsync();
    }
    public void SoftDelete(Category entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        _context.Categories.Update(entity);
    }
}