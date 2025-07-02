using AliyewShop.Application.DTOs.OrderDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.OrderValidators;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(o => o.ShippingAddress)
            .NotEmpty().WithMessage("Çatdırılma ünvanı mütləqdir.")
            .MaximumLength(1000).WithMessage("Çatdırılma ünvanı 1000 simvoldan çox ola bilməz.");

        RuleFor(o => o.PaymentType)
            .NotEmpty().WithMessage("Ödəniş növü mütləqdir.")
            .MaximumLength(100).WithMessage("Ödəniş növü 100 simvoldan çox ola bilməz.");

        RuleFor(o => o.InternalNote)
            .MaximumLength(1000).WithMessage("Daxili qeyd 1000 simvoldan çox ola bilməz.")
            .When(o => !string.IsNullOrEmpty(o.InternalNote));

        RuleFor(o => o.ProductIds)
            .NotEmpty().WithMessage("Sifarişdə ən azı bir məhsul olmalıdır.");
    }
}
