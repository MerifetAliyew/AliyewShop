using Microsoft.AspNetCore.Http;

namespace AliyewShop.Application.DTOs.FileDtos;

public record class FileUploadDto
{
    public IFormFile UploadFile { get; set; } = null!;
}