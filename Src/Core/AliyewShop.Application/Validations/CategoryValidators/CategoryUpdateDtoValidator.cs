using AliyewShop.Application.DTOs.CategoryDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.CategoryValidators;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Kateqoriya ID-si qeyd olunmalıdır.");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.")
            .MaximumLength(100).WithMessage("Kateqoriya adı maksimum 100 simvol ola bilər.");

        RuleFor(c => c.ParentCategoryId)
            .Must(id => id == null || id != Guid.Empty)
            .WithMessage("ParentCategoryId düzgün GUID olmalıdır.");
    }
}