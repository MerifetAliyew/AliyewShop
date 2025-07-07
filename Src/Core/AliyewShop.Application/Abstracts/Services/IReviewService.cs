using AliyewShop.Application.DTOs.ReviewDtos;
using AliyewShop.Application.Shared;

namespace AliyewShop.Application.Abstracts.Services;

public interface IReviewService
{
    Task<BaseResponse<string>> CreateReviewAsync(Guid productId, ReviewCreateDto dto, string userId);
    Task<BaseResponse<List<ReviewGetDto>>> GetReviewsByProductIdAsync(Guid productId);
}