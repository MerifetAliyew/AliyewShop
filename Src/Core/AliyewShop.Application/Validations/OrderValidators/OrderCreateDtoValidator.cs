using AliyewShop.Application.DTOs.OrderDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.OrderValidators;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.ProductIds)
            .NotEmpty().WithMessage("Məhsul siyahısı boş ola bilməz.")
            .Must(list => list.All(id => id != Guid.Empty))
            .WithMessage("Məhsul ID-ləri düzgün olmalıdır.");

        RuleFor(x => x.PaymentType)
            .NotEmpty().WithMessage("Ödəniş üsulu daxil edilməlidir.")
            .MaximumLength(100).WithMessage("Ödəniş üsulu 100 simvoldan artıq ola bilməz.");

        RuleFor(x => x.ShipToAddress)
            .NotEmpty().WithMessage("Çatdırılma ünvanı daxil edilməlidir.")
            .MaximumLength(500).WithMessage("Çatdırılma ünvanı 500 simvoldan artıq ola bilməz.");
    }
}
