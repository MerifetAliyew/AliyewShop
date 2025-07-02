namespace AliyewShop.Application.DTOs.CategoryDtos;

public class CategoryCreateDto
{
    public string Name { get; set; } // Kateqoriyanın adı
    public Guid? ParentCategoryId { get; set; } // Əgər bu alt kateqoriyadırsa, parent id-si
}
