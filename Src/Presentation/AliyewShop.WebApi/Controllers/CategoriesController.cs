using System.Net;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // POST api/categories
    [HttpPost]
    [Authorize(Policy = Permissions.Category.Create)]
    [ProducesResponseType(typeof(BaseResponse<CategoryUpdateDto>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CategoryCreateDto dto)
    {
        var result = await _categoryService.AddAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    // PUT api/categories/{id}
    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Category.Update)]
    [ProducesResponseType(typeof(BaseResponse<CategoryUpdateDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] CategoryUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new BaseResponse<string>("ID uyğun deyil", false, HttpStatusCode.BadRequest));
        }

        var result = await _categoryService.UpdateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    // GET api/categories?id=...
    [HttpGet]
    [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var result = await _categoryService.GetByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    // DELETE api/categories/{id}
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Category.Delete)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    // GET api/categories/search?search=...
    [HttpGet("search")]
    [ProducesResponseType(typeof(BaseResponse<List<CategoryGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> SearchByName([FromQuery] string search)
    {
        var result = await _categoryService.GetByNameSearchAsync(search);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("tree")]
    [ProducesResponseType(typeof(BaseResponse<List<CategoryTreeDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetTree()
    {
        var result = await _categoryService.GetCategoryTreeAsync();
        return StatusCode((int)result.StatusCode, result);
    }
}

