using AliyewShop.Application.DTOs.ProductDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.ProductValidatiors;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Məhsulun ID-si boş ola bilməz.");

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
            .MaximumLength(10).WithMessage("Ölçü maksimum 10 simvol ola bilər.");

        RuleFor(p => p.Color)
            .NotEmpty().WithMessage("Rəng qeyd olunmalıdır.")
            .MaximumLength(50).WithMessage("Rəng maksimum 50 simvol ola bilər.");

        RuleFor(p => p.Gender)
            .NotEmpty().WithMessage("Cinsiyyət qeyd olunmalıdır.")
            .MaximumLength(20).WithMessage("Cinsiyyət maksimum 20 simvol ola bilər.");

        RuleFor(p => p.Material)
            .NotEmpty().WithMessage("Material qeyd olunmalıdır.")
            .MaximumLength(100).WithMessage("Material maksimum 100 simvol ola bilər.");

        RuleFor(p => p.Season)
            .NotEmpty().WithMessage("Mövsüm qeyd olunmalıdır.")
            .MaximumLength(20).WithMessage("Mövsüm maksimum 20 simvol ola bilər.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Kateqoriya seçilməlidir.");

        RuleForEach(p => p.ImageUrls)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Şəkil URL-ləri düzgün formatda olmalıdır.")
            .When(p => p.ImageUrls != null && p.ImageUrls.Any());
    }
}
