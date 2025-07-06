using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.CategoryDtos;
using AliyewShop.Application.Shared;
using Microsoft.EntityFrameworkCore;
using AliyewShop.Domain.Entities;
using AutoMapper;

namespace AliyewShop.Persistence.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private ICategoryRepository _categoryRepository { get; }

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponse<string>> AddAsync(CategoryCreateDto dto)
    {
        if (dto.ParentCategoryId.HasValue)
        {
            var parentExists = await _categoryRepository.GetByIdAsync(dto.ParentCategoryId.Value);
            if (parentExists == null)
            {
                return new BaseResponse<string>("Parent category mövcud deyil", HttpStatusCode.BadRequest);
            }
        }

        var category = new Category
        {
            Name = dto.Name,
            ParentCategoryId = dto.ParentCategoryId
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<string>("Kateqoriya uğurla yaradıldı", true, HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var categoryDb = await _categoryRepository.GetByIdAsync(id);

        if (categoryDb is null)
        {
            return new BaseResponse<string>("Id not found", false, HttpStatusCode.NotFound);
        }

        _categoryRepository.Delete(categoryDb);
        await _categoryRepository.SaveChangeAsync();
        return new BaseResponse<string>("Successfully deleted", true, HttpStatusCode.OK); ;
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync()
    {
        var categories = _categoryRepository.GetAll();
        if (categories is null)
        {
            return new BaseResponse<List<CategoryGetDto>>(HttpStatusCode.NotFound);
        }
        var dtoList = new List<CategoryGetDto>();
        foreach (var category in categories)
        {
            dtoList.Add(new CategoryGetDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }
        return new BaseResponse<List<CategoryGetDto>>("Data", dtoList, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return new BaseResponse<CategoryGetDto>(HttpStatusCode.NotFound);
        }
        var dto = new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name
        };
        return new BaseResponse<CategoryGetDto>("Data", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetByNameAsync(string search)
    {
        var existCategories = await _categoryRepository
            .GetAll()
            .Where(c => c.Name.ToLower().Contains(search.Trim().ToLower()))
            .ToListAsync();

        if (existCategories == null || !existCategories.Any())
        {
            return new BaseResponse<List<CategoryGetDto>>("Category name not found", false, HttpStatusCode.NotFound);
        }

        var categoryDtos = _mapper.Map<List<CategoryGetDto>>(existCategories);

        return new BaseResponse<List<CategoryGetDto>>("Successfully", categoryDtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetByNameSearchAsync(string namePart)
    {
        var categories = await _categoryRepository.GetByNameSearchAsync(namePart);
        if (categories == null || !categories.Any())
        {
            return new BaseResponse<List<CategoryGetDto>>("No categories found with the given name part", HttpStatusCode.NotFound);
        }

        return new BaseResponse<List<CategoryGetDto>>("Data", _mapper.Map<List<CategoryGetDto>>(categories), HttpStatusCode.OK);
    }

    public async Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto dto)
    {
        var categoryDb = await _categoryRepository.GetByIdAsync(dto.Id);
        if (categoryDb is not null)
        {
            return new BaseResponse<CategoryUpdateDto>(HttpStatusCode.NotFound);
        }

        var existedCategory = await _categoryRepository
            .GetByFiltered(c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower())
            .FirstOrDefaultAsync();
        if (existedCategory is not null)
        {
            return new BaseResponse<CategoryUpdateDto>("This category already exists", HttpStatusCode.BadRequest);
        }
        categoryDb.Name = dto.Name.Trim();



        await _categoryRepository.SaveChangeAsync();
        return new BaseResponse<CategoryUpdateDto>("Category updated successfully", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryTreeDto>>> GetCategoryTreeAsync()
    {
        var allCategories = await _categoryRepository
            .GetAll(true)
            .Include(c => c.SubCategories)
            .ToListAsync();

        var mainCategories = allCategories
            .Where(c => c.ParentCategoryId == null)
            .ToList();

        var treeList = mainCategories
            .Select(main => BuildTree(main, allCategories))
            .ToList();

        return new BaseResponse<List<CategoryTreeDto>>("Kateqoriyalar ağacı uğurla yaradıldı", treeList, HttpStatusCode.OK);
    }

    private CategoryTreeDto BuildTree(Category category, List<Category> allCategories)
    {
        return new CategoryTreeDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            SubCategories = allCategories
                .Where(sub => sub.ParentCategoryId == category.Id)
                .Select(sub => BuildTree(sub, allCategories))
                .ToList()
        };
    }
}
