using AliyewShop.Application.DTOs.OrderProductDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.OrderValidators;

public class OrderProductUpdateDtoValidator : AbstractValidator<OrderProductUpdateDto>
{
    public OrderProductUpdateDtoValidator()
    {
        RuleFor(op => op.Id)
            .NotEmpty().WithMessage("OrderProduct ID-si mütləqdir.");

        RuleFor(op => op.ProductCount)
            .GreaterThan(0).WithMessage("Məhsul sayı 0-dan böyük olmalıdır.");

        RuleFor(op => op.ProductPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Məhsulun qiyməti sıfır və ya ondan böyük olmalıdır.");
    }
}