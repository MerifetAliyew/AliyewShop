using AliyewShop.Application.DTOs.FileDtos;
using FluentValidation;

namespace AliyewShop.Application.Validations.FileUploadValidator;

public class FileUploadValidator : AbstractValidator<FileUploadDto>
{
    private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".pdf" };
    private const long _maxFileSize = 5 * 1024 * 1024; // 5 MB

    public FileUploadValidator()
    {
        RuleFor(f => f.UploadFile)
            .NotNull().WithMessage("Fayl seçilməlidir.")
            .Must(f => f != null && f.Length > 0)
            .WithMessage("Fayl boş ola bilməz.")
            .Must(f => f != null && f.Length <= _maxFileSize)
            .WithMessage($"Fayl ölçüsü maksimum {_maxFileSize / (1024 * 1024)} MB ola bilər.")
            .Must(f => f != null && _allowedExtensions
            .Contains(Path.GetExtension(f.FileName).ToLower()))
            .WithMessage("Yalnız .jpg, .jpeg, .png, .pdf formatları qəbul edilir.");
    }
}