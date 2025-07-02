using AliyewShop.Application.DTOs.CategoryDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.CategoryValidators;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.")
            .MaximumLength(100).WithMessage("Kateqoriya adı maksimum 100 simvol ola bilər.");

        RuleFor(c => c.ParentCategoryId)
            .Must(id => id == null || id != Guid.Empty)
            .WithMessage("ParentCategoryId düzgün GUID olmalıdır.");
    }
}