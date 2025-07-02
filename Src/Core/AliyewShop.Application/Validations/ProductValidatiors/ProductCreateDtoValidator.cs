using AliyewShop.Application.DTOs.ProductDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.ProductValidatiors;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Məhsulun adı boş ola bilməz.")
            .MaximumLength(150).WithMessage("Məhsulun adı maksimum 150 simvol ola bilər.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Məhsulun açıqlaması boş ola bilməz.")
            .MaximumLength(2000).WithMessage("Açıqlama maksimum 2000 simvol ola bilər.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Qiymət sıfırdan böyük olmalıdır.");

        RuleFor(p => p.StockCount)
            .GreaterThanOrEqualTo(0).WithMessage("Stok sayı mənfi ola bilməz.");

        RuleFor(p => p.Size)
            .NotEmpty().WithMessage("Ölçü qeyd olunmalıdır.")
            .MaximumLength(10);

        RuleFor(p => p.Color)
            .NotEmpty().WithMessage("Rəng qeyd olunmalıdır.")
            .MaximumLength(50);

        RuleFor(p => p.Gender)
            .NotEmpty().WithMessage("Cinsiyyət qeyd olunmalıdır.")
            .MaximumLength(20);

        RuleFor(p => p.Material)
            .NotEmpty().WithMessage("Material qeyd olunmalıdır.")
            .MaximumLength(100);

        RuleFor(p => p.Season)
            .NotEmpty().WithMessage("Mövsüm qeyd olunmalıdır.")
            .MaximumLength(20);

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Kateqoriya seçilməlidir.");
    }
}