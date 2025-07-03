using AliyewShop.Application.DTOs.UserDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.UserValidators;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName boş ola bilməz")
            .MaximumLength(100).WithMessage("FullName maksimum 100 simvol ola bilər");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .EmailAddress().WithMessage("Email formatı düzgün deyil");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz")
            .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol olmalıdır");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Rol yalnız Buyer və ya Seller ola bilər");
    }
}
