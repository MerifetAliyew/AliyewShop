namespace AliyewShop.Application.DTOs.CategoryDtos;

public class CategoryUpdateDto
{
    public Guid Id { get; set; } // Dəyişdiriləcək kateqoriyanın id-si
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
}