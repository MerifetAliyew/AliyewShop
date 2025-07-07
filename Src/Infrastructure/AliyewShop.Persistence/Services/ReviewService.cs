using AliyewShop.Application.Abstracts.Repositories;
using System.Net;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.ReviewDtos;
using AliyewShop.Application.Shared;
using AutoMapper;
using AliyewShop.Domain.Entities;

namespace AliyewShop.Persistence.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ReviewService(IReviewRepository reviewRepository, IProductRepository productRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<string>> CreateReviewAsync(Guid productId, ReviewCreateDto dto, string userId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            return new("Məhsul tapılmadı", HttpStatusCode.NotFound);

        var review = new Review
        {
            ProductId = productId,
            UserId = userId,
            CommentBody = dto.CommentBody,
            Rating = dto.Rating,
            IsConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };

        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangeAsync();

        return new("Review əlavə olundu. Admin təsdiqləməlidir.", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<List<ReviewGetDto>>> GetReviewsByProductIdAsync(Guid productId)
    {
        var reviews = await _reviewRepository.GetConfirmedReviewsByProductIdAsync(productId);

        var dtos = reviews.Select(r => new ReviewGetDto
        {
            Id = r.Id,
            UserFullName = r.User.Fullname,
            CommentBody = r.CommentBody,
            Rating = r.Rating,
            CreatedAt = r.CreatedAt
        }).ToList();

        return new("Review siyahısı", dtos, HttpStatusCode.OK);
    }
}
