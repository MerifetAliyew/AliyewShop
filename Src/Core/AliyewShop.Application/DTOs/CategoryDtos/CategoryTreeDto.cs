namespace AliyewShop.Application.DTOs.CategoryDtos;

public record class CategoryTreeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CategoryTreeDto> SubCategories { get; set; } = new List<CategoryTreeDto>();
}
