namespace AliyewShop.Application.DTOs.CategoryDtos;

public record class CategoryUpdateDto
{
    public Guid Id { get; set; } // Dəyişdiriləcək kateqoriyanın id-si
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}