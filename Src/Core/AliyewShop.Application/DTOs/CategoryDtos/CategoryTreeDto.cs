namespace AliyewShop.Application.DTOs.CategoryDtos;

public record class CategoryTreeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public List<CategoryTreeDto> SubCategories { get; set; } = new();
}
