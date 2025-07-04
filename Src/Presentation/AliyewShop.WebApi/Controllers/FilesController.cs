using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.FileDtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileServices;

    public FilesController(IFileService fileServices)
    {
        _fileServices = fileServices;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAsync([FromForm] FileUploadDto dto)
    {

        var fileUrl = await _fileServices.UploadAsync(dto.UploadFile);
        return Ok(fileUrl);
    }
}
