using AliyewShop.Application.DTOs.OrderProductDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.OrderProductValidators;

public class OrderProductCreateDtoValidator : AbstractValidator<OrderProductCreateDto>
{
    public OrderProductCreateDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId boş ola bilməz.");
        RuleFor(x => x.ProductCount).GreaterThan(0)
            .WithMessage("Məhsul sayı 0-dan böyük olmalıdır.");
        RuleFor(x => x.ProductPrice).GreaterThanOrEqualTo(0)
            .WithMessage("Məhsul qiyməti mənfi ola bilməz.");
    }
}