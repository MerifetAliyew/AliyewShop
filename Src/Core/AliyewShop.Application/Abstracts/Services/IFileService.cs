using Microsoft.AspNetCore.Http;

namespace AliyewShop.Application.Abstracts.Services;

public interface IFileService
{
    Task<string> UploadAsync(IFormFile file);
}
